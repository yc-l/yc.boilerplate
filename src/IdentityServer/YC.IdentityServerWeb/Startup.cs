using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YC.IdentityServerWeb.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;

namespace YC.IdentityServerWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 26)));
            });

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var rsaCert = new X509Certificate2("./keys/identityserver.test.rsa.p12", "changeit");
            var key = new X509SecurityKey(rsaCert);
            
            var credential = new SigningCredentials(key, "PS256");
          
            var builder = services.AddIdentityServer();
            builder //.AddDeveloperSigningCredential()//开发者证书，针对token签名，动态
                .AddSigningCredential(rsaCert, "PS256")//生产证书，静态
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = optionsBuilder =>
                        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 26)),
                            contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("YC.IdentityServerWeb"));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = optionsBuilder =>
                        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 26)),
                            contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("YC.IdentityServerWeb"));
                })
                .AddAspNetIdentity<AppUser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //最初数据没有时候执行，有的话就跳过
            SeedData.InitIdentityServerDb(app);
            SeedData.InitAspNetIdentityDb(app);
            //自定义修改调整
           // SeedData.ChangeAspNetIdentityDb(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(builder =>
            {
                builder.MapDefaultControllerRoute();
            });
        }
    }
}