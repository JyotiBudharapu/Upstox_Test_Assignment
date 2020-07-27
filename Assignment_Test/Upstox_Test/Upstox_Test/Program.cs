using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Topshelf;
using Upstox_Service.Dtos;
using Upstox_Test.Helpers;
using Upstox_Test.Providers;
using Upstox_Test.Services;

namespace Upstox_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HostFactory.Run(hostConfigurator =>
                {
                    hostConfigurator.Service(settings =>
                    {
                       
                        var serviceCollection = new ServiceCollection();
                        IConfiguration configuration = Configuration();
                        serviceCollection.Configure<Settings>(configuration.GetSection("Settings"));
                        var serviceProvider = ServiceHelper.CreateServiceProvider(serviceCollection);


                        var logger = serviceProvider.GetService<ILogger>();
                        logger.Debug("In HostFactory.Run startings runner.");
                        // Configuration provider...
                        var runner = serviceProvider.GetService<WinRunner>();

                        return new ServiceWrapper(runner);
                    },
                    serviceConfigurator =>
                    {
                        serviceConfigurator.BeforeStartingService(_ => Console.WriteLine("BeforeStart"));
                        serviceConfigurator.BeforeStoppingService(_ => Console.WriteLine("BeforeStop"));
                    });
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Log.Error(ex, "failed to start.");
            }
        }

        public static IConfiguration Configuration()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

            return config;
        }
       
    }
}
