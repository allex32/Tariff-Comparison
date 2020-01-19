using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TC.WebApiInspector.Configuration;
using TC.WebApiInspector.Inspectors;

namespace TC.WebApiInspector
{
    class Program
    {
        static HashSet<string> ApiModes = new HashSet<string>(new[] { "actual", "fake" });
        
        static async Task Main(string[] args)
        {
            var apiMode = ParseApiModeInput();
            var serviceProvider = RegisterServices(apiMode);

            var startup = serviceProvider.GetService<Startup>();
            await startup.Run();

            Console.WriteLine("Enter any key to exit");
            Console.ReadLine();
        }

        private static string ParseApiModeInput()
        {
            var infoString =
@"The test application can be run against the fake or actual api endpoints.
Base urls for the api endpoinds must be specified in the corresponding section in appsettings.json file.
Fake api endpoint can be launched using the powershell script included in the project.
Print one of the following commands {'actual', 'fake'} to run the app against the corresponding api endpoint.";

            Console.WriteLine(infoString);

            var apiModeInput = Console.ReadLine();
            
            if(!ApiModes.Contains(apiModeInput, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentOutOfRangeException("The command must have one of the following values : {'actual', 'fake'}");
            }

            return apiModeInput;          
        }

        static IServiceProvider RegisterServices(string apiEndpointMode)
        {
            var collection = new ServiceCollection()
                .AddLogging(configure => 
                {
                    configure.ClearProviders();
                    configure.AddConsole();
                });
            
            collection.AddScoped<Startup>();

            collection.Scan(scan => scan
                .FromAssemblyOf<IInspector>()
                .AddClasses(classes => classes.AssignableTo<IInspector>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            collection.Configure<ApiEndpointConfiguration>(GetConfigurationRoot()
                .GetSection("application")
                .GetSection("ApiEndpoint")
                .GetSection(apiEndpointMode));

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
