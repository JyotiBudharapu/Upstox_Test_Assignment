using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Upstox_Service.Services;
using Upstox_Test.Monitors;
using Upstox_Test.Providers;

namespace Upstox_Test.Helpers
{
    public class ServiceHelper
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        static IConfigurationRoot _configuration => new ConfigurationBuilder()
                                                     .AddJsonFile("appsettings.json")
                                                    .Build();

        public static IConfiguration Configuration()
        {
            var config = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

            return config;
        }

        

        public static IServiceProvider CreateServiceProvider(ServiceCollection services)
        {
            var loggerFactory = new LoggerFactory().AddSerilog();
            services
                .AddSingleton(loggerFactory)
                .AddLogging()
                .AddSingleton(c => Log.Logger);
            services.AddSingleton<Task>();

            services.AddTransient<IMonitor, ProcessBarChatMonitor>();
            services.AddTransient(s =>
            {
                var logger = s.GetService<Serilog.ILogger>();

                var monitor = s.GetService<IEnumerable<IMonitor>>();
                var tasks = new List<Task>();
                return new WinRunner(monitor, logger, tasks);
            });

            var serviceProvider = services.BuildServiceProvider();
            var configurationProvider = serviceProvider.GetService<IConfigurationProvider>();
            ConfigureLogging(configurationProvider);
            return serviceProvider;
        }

       

        private static void ConfigureLogging(IConfigurationProvider configurationProvider)
        {
          
            var fileName = $"C:\\logs\\Test_Assignment" + "{Date}.log";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile(fileName)
                .ReadFrom.Configuration(_configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
