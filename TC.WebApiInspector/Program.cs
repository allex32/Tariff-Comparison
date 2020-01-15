using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TC.WebApiInspector.Configuration;
using TC.WebApiInspector.Inspectors;

namespace TC.WebApiInspector
{
    class Program
    {
        static void Main(string[] args)
        {        
            var serviceProvider = RegisterServices();

            var startup = serviceProvider.GetService<Startup>();
            startup.Run();

            Console.WriteLine("Press any button to exit");
            Console.ReadKey();
        }

        static IServiceProvider RegisterServices()
        {
            var collection = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole());
            
            collection.AddScoped<Startup>();
            collection.Scan(scan => scan
                .FromAssemblyOf<IInspector>()
                .AddClasses(classes => classes.AssignableTo<IInspector>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            collection.Configure<ApiEndpointConfiguration>(GetConfigurationRoot().GetSection("application"));
           
            return collection.BuildServiceProvider();
        }

        static IConfigurationRoot GetConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

             return builder.Build();
        }

    }
}
