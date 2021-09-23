using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Text;

namespace YC.Log.Serilog
{
    /// <summary>
    /// Serilog 日志拓展
    /// </summary>
    public static class SerilogHostingExtensions
    {
        /// <summary>
        /// 添加默认日志拓展
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configAction"></param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder UseSerilogDefault(this IWebHostBuilder hostBuilder, Action<LoggerConfiguration> configAction = default)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");//按时间创建文件夹
            string outputTemplate = "{NewLine}【{Level:u3}】{Timestamp:yyyy-MM-dd HH:mm:ss.fff}" +
            "{NewLine}#Msg#{Message:lj}" +
            "{NewLine}#Pro #{Properties:j}" +
            "{NewLine}#Exc#{Exception}" +
            new string('-', 50);//输出模板
            hostBuilder.UseSerilog((context, configuration) =>
            {
                // 加载配置文件
                var config = configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext();
               
                if (configAction != null) configAction.Invoke(config);
                else
                {
                    //筛选过滤
                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information).WriteTo.Console()
                          .WriteTo.File($"Logs/{date}/{LogEventLevel.Information}.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Infinite, encoding: Encoding.UTF8));

                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug).WriteTo.Console()
                         .WriteTo.File($"Logs/{date}/{LogEventLevel.Debug}.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Infinite, encoding: Encoding.UTF8));

                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning).WriteTo.Console()
                         .WriteTo.File($"Logs/{date}/{LogEventLevel.Warning}.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Infinite, encoding: Encoding.UTF8));

                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error).WriteTo.Console()
                        .WriteTo.File($"Logs/{date}/{LogEventLevel.Error}.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Infinite, encoding: Encoding.UTF8));
                }
            });

            return hostBuilder;
        }

        /// <summary>
        /// 添加默认日志拓展
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static IHostBuilder UseSerilogDefault(this IHostBuilder builder, Action<LoggerConfiguration> configAction = default)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");//按时间创建文件夹
            string outputTemplate = "{NewLine}【{Level:u3}】{Timestamp:yyyy-MM-dd HH:mm:ss.fff}" +
            "{NewLine}#Msg#{Message:lj}" +
            "{NewLine}#Pro #{Properties:j}" +
            "{NewLine}#Exc#{Exception}" +
            new string('-', 50);//输出模板
            builder.UseSerilog((context, configuration) =>
            {
                // 加载配置文件
                var config = configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext();

                if (configAction != null) configAction.Invoke(config);
                else
                {
                    //筛选过滤
                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information).WriteTo.Console()
                          .WriteTo.File($"Logs/{date}/{LogEventLevel.Information}.log", outputTemplate: outputTemplate,rollingInterval: RollingInterval.Infinite,encoding: Encoding.UTF8));
                    
                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug).WriteTo.Console()
                         .WriteTo.File($"Logs/{date}/{LogEventLevel.Debug}.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Infinite, encoding: Encoding.UTF8));

                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning).WriteTo.Console()
                         .WriteTo.File($"Logs/{date}/{LogEventLevel.Warning}.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Infinite, encoding: Encoding.UTF8));

                    config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error).WriteTo.Console()
                        .WriteTo.File($"Logs/{date}/{LogEventLevel.Error}.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Infinite, encoding: Encoding.UTF8));
                }
            });

            return builder;
        }
    }
}
