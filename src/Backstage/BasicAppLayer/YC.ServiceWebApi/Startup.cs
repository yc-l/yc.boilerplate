using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using YC.ServiceWebApi.Filter;
using ServiceWebApi;
using YC.ApplicationService;
using YC.ApplicationService.ApplicationService;
using YC.Core.Autofac;
using YC.Core.HttpExtensions;
using YC.DapperFrameWork;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using YC.Core.Cache;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection;
using System.Configuration;
using YC.Cache.Redis;
using Microsoft.Extensions.Caching.Redis;
using YC.ApplicationService.DefaultConfigure;

using Autofac.Multitenant;
using YC.ServiceWebApi.Tenant;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YC.ServiceWebApi.Middleware;
using YC.ServiceWebApi.ServiceCollectionExtensions;
using YC.Core;
using YC.FreeSqlFrameWork;
using FreeSql;
using YC.ServiceWebApi.AopModule;
using Serilog;
using YC.Core.DynamicApi;
using YC.ServiceWebApi.Extensions;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using System.Threading.Tasks;
using YC.QuartzService.Interface;
using YC.QuartzService.JobService.CreateDirJobService;
using YC.QuartzService.JobService.WriteFileJobService;
using YC.QuartzService.JobService.DeleteLogJobService;
using YC.QuartzServiceModule;
using Quartz;
using YC.QuartzServiceModule.Model;

namespace YC.ServiceWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // 可选初始化配置
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            OptionConfigure(services);

            // 设置允许所有来源跨域
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(_ => true) // =AllowAnyOrigin()
                    .AllowCredentials();
            }));

            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                 .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //全局静态配置类，第一次配置，如果变更，需要重新启动项目，或者重新给JsonConfig 赋值 
            DefaultConfig.JsonConfig = DefaultConfig.GetConfigJson(DefaultConfig.dbConfigFilePath);


            #region 使用Redis保存Session
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => false; //这里要改为false，默认是true，true的时候session无效
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.AddDistributedRedisCache(option =>
            //{
            //    //redis 连接字符串
            //    option.Configuration = dbConfig.ConnectionRedis.Connection;
            //    //redis 实例名
            //    option.InstanceName = dbConfig.ConnectionRedis.InstanceName;
            //}
            //);

            ////添加session 设置过期时长分钟  
            //var sessionOutTime = dbConfig.ConnectionRedis.SessionTimeOut;
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToDouble(sessionOutTime)); //session活期时间
            //    options.Cookie.HttpOnly = true;//设为httponly
            //});
            #endregion

            //默认的session 添加session 和请求上下文的注入
            services.AddSession();
            // services.AddHttpContextAccessor();

            //添加缓存
            services.AddMemoryCache();

            //全局过滤器注入
            services.AddMvc(options =>
             {
                 options.EnableEndpointRouting = false;
                 options.Filters.Add(typeof(AOPResourceFilterAttribute));
                 options.Filters.Add(typeof(AOPActionFilterAttribute));
                 options.Filters.Add(typeof(AOPResultFilterAttribute));
             });

            //全局配置Json序列化处理
            services.AddDirectoryBrowser();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              
            });

            #region  配置webapi 多版本

            //services.AddApiVersioning(options =>
            //{
            //    options.ReportApiVersions = true;
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //});
            #endregion

            #region swagger 配置
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API接口文档",
                    Version = "v1",
                    Description = "API v1",
                    Contact = new OpenApiContact { Name = "", Email = "" }
                });

                //添加Jwt验证设置
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Value: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // TODO:一定要返回true！
                options.DocInclusionPredicate((docName, description) => true);

                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = System.AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            });

            #endregion

            #region 动态WebApi

            // 默认配置
            services.AddDynamicWebApi((options) =>
            {

                options.AddAssemblyOptions(this.GetType().Assembly, httpVerb: "POST");
            });
            // 自定义配置
            //services.AddDynamicWebApi((options) =>
            //{
            //    // 指定全局默认的 api 前缀
            //    options.DefaultApiPrefix = "api";

            //    /**
            //     * 清空API结尾，不删除API结尾;
            //     * 若不清空 CreatUserAsync 将变为 CreateUser
            //     */
            //    options.RemoveActionPostfixes.Clear();

            //    /**
            //     * 自定义 ActionName 处理函数;
            //     */
            //    options.GetRestFulActionName = (actionName) => actionName;

            //    /**
            //     * 指定程序集 配置 url 前缀为 apis
            //     * 如: http://localhost:8080/apis/User/CreateUser
            //     */
            //    options.AddAssemblyOptions(this.GetType().Assembly, apiPreFix: "api");

            //    /**
            //     * 指定程序集 配置所有的api请求方式都为 POST
            //     */
            //    options.AddAssemblyOptions(this.GetType().Assembly, httpVerb: "POST");

            //    /**
            //     * 指定程序集 配置 url 前缀为 apis, 且所有请求方式都为POST
            //     * 如: http://localhost:8080/apis/User/CreateUser
            //     */
            //    options.AddAssemblyOptions(this.GetType().Assembly, apiPreFix: "api", httpVerb: "POST");
            //}); 
            #endregion

            #region 1、IdentityServer
            //services.AddMvcCore()
            //  .AddAuthorization()
            //  .AddJsonFormatters();

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "http://localhost:9000";
            //        options.RequireHttpsMetadata = false;

            //        options.Audience = "api1";
            //    });

            //services.AddCors(options =>
            //{
            //    // this defines a CORS policy called "default"
            //    options.AddPolicy("default", policy =>
            //    {
            //        policy.WithOrigins("http://localhost:4200")
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //    });
            //});
            #endregion

            #region 2、autofac dependencyInjection

            //实例化Autofac容器
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependencyInjectionSupport);
            //var dynamicWebApiType = typeof(IDynamicWebApi);

            #region 1、 加载类

            List<Assembly> assemblyList = new List<Assembly>();
            //但是实际上，很多项目都会把项目分到其他的类库里面，如果使用Assembly.GetEntryAssembly();，放到其他的项目里面的可以被注入的类就无法被注入了，所以需要手动去加载其他的程序集，当然也可以用代码去扫描一下
            assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("YC.ApplicationService")));
            assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("YC.DapperFrameWork")));
            assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("YC.FreeSqlFrameWork")));
            assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("YC.ServiceWebApi")));
            assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("YC.QuartzService")));
            foreach (var assembly in assemblyList)
            {
                builder.RegisterAssemblyTypes(assembly).Where(x => (baseType.IsAssignableFrom(x) && x.IsClass))
              .AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();//指明创建的stypes这个集合中所有类的对象实例，以其接口的形式保存  

                foreach (var repo in assembly.GetTypes().Where(a => (a.IsAbstract == false && typeof(IBaseRepository).IsAssignableFrom(a))))
                    services.AddScoped(repo);//freesql的注入

            }
           
            #endregion
            #region 2、注入操作

            builder.RegisterModule(new CustomAutofacModule());

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            //automapper 注入
            builder.RegisterModule(new AutoMapperAutofacModule());

             

            //dapper 注入
            builder.RegisterModule(new DapperAutofacModule());

            //freesql 注入
            builder.RegisterModule(new FreesqlAutofacModule());
            //Quartz服务注入
            builder.RegisterModule(new QuartzModule());
            var idle = services.AddTenantDb();//租户注入
            builder.RegisterInstance(idle).SingleInstance();//单例注册 

            //最后的注入处理
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());//注入当前程程序集
            builder.Populate(services);// 这个是重点
            var container = builder.Build();

            AutofacUtils.Container = container;
            return new AutofacServiceProvider(container);//那就返回默认的注入模式


            #endregion
            #endregion


        }


        public IQuartzRepository _quartzRepository;
        public IScheduler _scheduler;

        // 必选，
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<CorsOptions> corsOptions, IScheduler _scheduler, IQuartzRepository quartzRepository)
        {
            #region 1、使用静态页面
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseStaticFiles();
            app.UseSerilogRequestLogging();    // 必须在 UseStaticFiles 和 UseRouting 之间
            //路由
            app.UseRouting();

          
            // 使用跨域配置
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            // app.UseCors("default");
            app.UseAuthentication();

            //使用中间件全局异常过滤器
            app.UseMiddleware<ExceptionMiddleware>();

        
            _quartzRepository = quartzRepository;
            await _scheduler.Start();

            //List<QuartzJobsCollection> jobLibraysList = new List<QuartzJobsCollection>();
            //jobLibraysList.Add(new InitDbJobsConfig().InitDbJob);

            //var list = await _quartzRepository.DefaultRunningServer(jobLibraysList);

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "api/[area]/[controller]/[action]/{id?}");
            //});

            //配置端点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //配置监听客户端ip
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                KnownProxies = { IPAddress.Parse(DefaultConfig.DefaultAppConfigDto.NginxAgentIP) }
            });

            // 添加NLog日志支持
            //loggerFactory.AddNLog();


            //在这里注入，保证是上面的单例，如果在上面的IServiceProvider ConfigureServices(IServiceCollection services)
            //进行services.BuildServiceProvider()就会出现非单例
            AutofacUtils.Configure(app.ApplicationServices);


            //添加swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI V1");

            });

           

        }

        //跨域文件模式版本配置
        private void OptionConfigure(IServiceCollection services)
        {
            services.Configure<CorsOptions>(Configuration.GetSection("AllowedHosts"));
        }



}
}
