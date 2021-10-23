using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YC.Micro.UserWebService;
using YC.Micro.UserWebService.AopModule;
using YC.Micro.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using YC.Micro.UserWebService.ServiceCollectionExtensions;

namespace YC.Micro.OrderWebService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DefaultConfig.JsonConfig = DefaultConfig.GetConfigJson(DefaultConfig.dbConfigFilePath);
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var tenantSetting=configuration.GetSection("TenantSetting").Get<TenantSetting>();

         
            services.AddGrpc();
            #region Autofac IOC 注入

            var builder = new ContainerBuilder();

            //自定义注入
             builder.RegisterModule(new CustomAutofacModule());

            //automapper 注入
            builder.RegisterModule(new AutoMapperAutofacModule());

            //freesql 注入
            builder.RegisterModule(new FreesqlAutofacModule());
            var idle = services.AddTenantDb();//租户注入
            builder.RegisterInstance(idle).SingleInstance();//单例注册 
            //最后的注入处理
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());//注入当前程程序集
            builder.Populate(services);// 这个是重点
            var container = builder.Build();

            #endregion


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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<UserService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
