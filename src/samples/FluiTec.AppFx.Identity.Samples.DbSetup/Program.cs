using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Identity.Samples.DbSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            // load config from json
            var path = GetApplicationRoot();
            Console.WriteLine($"BasePath: {path}");
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.secret.json", false, true)
                .Build();
            
            // configure
            var manager = new ConsoleReportingConfigurationManager(config);
            var services = new ServiceCollection();
            services.ConfigureDynamicIdentityDataProvider(manager);

            // finish di
            var sp = services.BuildServiceProvider();

            var dataService = sp.GetRequiredService<IIdentityDataService>();
            using (var uow = dataService.BeginUnitOfWork())
            {
            }
        }

        /// <summary>   Gets application root. </summary>
        /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
        ///                                                 invalid. </exception>
        /// <returns>   The application root. </returns>
        private static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath ?? throw new InvalidOperationException()).Value;
                return appRoot;
            }
            else
            {
                var appPathMatcher = new Regex(@"(?<!file)\/+[\S\s]*?(?=\/+bin)");
                var appRoot = appPathMatcher.Match(exePath ?? throw new InvalidOperationException()).Value;
                return appRoot;
            }
        }
    }
}
