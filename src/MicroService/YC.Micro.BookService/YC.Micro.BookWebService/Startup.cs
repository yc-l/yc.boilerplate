using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YC.Core.Autofac;
using YC.Micro.BookWebService;
using YC.Micro.BookWebService.AopModule;
using YC.Micro.BookWebService.ServiceCollectionExtensions;

using YC.Micro.Configuration;
using YC.Micro.Consul.ServiceRegister.Extentions;
using YC.Micro.Core;

namespace YC.Micro.BookWebService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DefaultConfig.JsonConfig = DefaultConfig.GetConfigJson(DefaultConfig.dbConfigFilePath);
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var tenantSetting = configuration.GetSection("TenantSetting").Get<TenantSetting>();

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<AuthenticationInterceptor>();
            });
            services.AddControllers();
            services.AddAuthorization();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                          .AddIdentityServerAuthentication(options =>
                          {
                              options.Authority = "https://localhost:5001";
                              //options.RequireHttpsMetadata = false;
                          });
            //使用健康检查
            //services.AddHealthChecks();

            #region Autofac IOC 注入

            var builder = new ContainerBuilder();
            services.AddDependOnModule(builder);//模块注入
            var idle = services.AddTenantDb();//租户注入
            services.AddServiceRegister(); //consul 注册服务
            builder.RegisterInstance(idle).SingleInstance();//单例注册
            //最后的注入处理
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());//注入当前程程序集
            builder.Populate(services);// 这个是重点
            var container = builder.Build();

            #endregion Autofac IOC 注入

            return new AutofacServiceProvider(container);//那就返回默认的注入模式
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();//重点这两个
            app.UseAuthorization();//重点这两个
            // 添加健康检查路由地址
            app.Map("/HealthCheck", HealthMap);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BookService>();
                //endpoints.MapHealthChecks("/HealthCheck");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        private static void HealthMap(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("OK");
            });
        }
    }
}