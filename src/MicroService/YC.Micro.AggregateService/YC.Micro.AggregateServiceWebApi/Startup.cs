using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YC.Micro.AggregateServiceWebApi.AopModule;
using YC.Micro.AggregateServiceWebApi.ServiceCollectionExtensions;
using YC.Micro.Configuration;
using YC.Micro.Consul.ServiceRegister.Extentions;

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

            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = "https://localhost:5001";
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = false
                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", builder =>
                {
                    builder.RequireAuthenticatedUser();//需要odic 身份认证
                    builder.RequireClaim("scope", "test_api");//需要对应的域
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YC.Micro.AggregateServiceWebApi", Version = "v1" });
                //添加Jwt验证设置
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                        });
                //swagger 那边的值直接填写 token值，不要写Bearer token的内容
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Value: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // TODO:一定要返回true！
                c.DocInclusionPredicate((docName, description) => true);
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YC.Micro.AggregateServiceWebApi v1"));
            }

            app.UseRouting();

            //身份认证
            app.UseAuthentication();
            app.UseAuthorization();
            // 添加健康检查路由地址
            app.Map("/HealthCheck", HealthMap);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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