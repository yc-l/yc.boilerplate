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
using YC.Micro.Core;
using YC.Micro.Consul.ServiceRegister.Extentions;
using YC.Core.Autofac;
using IdentityServer4.AccessTokenValidation;
using System.Net.Http;

namespace YC.Micro.UserWebService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DefaultConfig.JsonConfig = DefaultConfig.GetConfigJson(DefaultConfig.dbConfigFilePath);
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //program 注入文件也是要使用时候使用ioc 示例化一个新的，保证实时性
            var tenantSetting = configuration.GetSection("TenantSetting").Get<TenantSetting>();
            var data = configuration.GetSection("TenantSetting").GetChildren().ToList();//只获取一层数据，如果内部有嵌套，需要使用model 去获取，或者使用如下方式获取
            //这个可以使用AsEnumerable变为集合，之后去查找，也可以直接就foreach;进行遍历
            var tenantList = configuration.GetSection("TenantSetting").AsEnumerable().Where(x => x.Key.Contains("TenantSetting:TenantList")).Select(x => x.Value);

            var conn = configuration.GetSection("TenantSetting:DefaultDbConnectionString").Value;//获取指定的Key内容

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
            services.AddServiceRegister(); //consul 注册服务
            var idle = services.AddTenantDb();//租户注入
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
            app.UseAuthentication();
            app.UseAuthorization();
            // 添加健康检查路由地址
            app.Map("/HealthCheck", HealthMap);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<UserService>();
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
                var httpHandler = new HttpClientHandler();
                // Return `true` to allow certificates that are untrusted/invalid
                httpHandler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                await context.Response.WriteAsync("OK");
            });
        }
    }
}