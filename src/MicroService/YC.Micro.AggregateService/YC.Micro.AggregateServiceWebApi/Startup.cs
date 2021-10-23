using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YC.Micro.AggregateServiceWebApi.AopModule;
using YC.Micro.AggregateServiceWebApi.ServiceCollectionExtensions;
using YC.Micro.Configuration;

namespace YC.Micro.AggregateServiceWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DefaultConfig.JsonConfig = DefaultConfig.GetConfigJson(DefaultConfig.dbConfigFilePath);
        
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YC.Micro.AggregateServiceWebApi", Version = "v1" });
            });

            #region Autofac IOC 注入

            var builder = new ContainerBuilder();

            //自定义注入
            builder.RegisterModule(new CustomAutofacModule());

            //automapper 注入
            builder.RegisterModule(new AutoMapperAutofacModule());

            //freesql 注入
            builder.RegisterModule(new FreesqlAutofacModule());

            //es 注入
            builder.RegisterModule(new ElasticSearchAutofacModule());

            //GRpc 服务注入
            services.AddGrpcModule();

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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YC.Micro.AggregateServiceWebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
