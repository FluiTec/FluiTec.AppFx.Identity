using System;
using System.Collections.Generic;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Identity.Sample.DbSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbServer = "marble.fritz.box";
            var dbCatalog = "wtschnell";

            var configValues = new List<KeyValuePair<string, string>>(new[]
            {
                new KeyValuePair<string, string>("DynamicDataOptions:Provider", "Mssql"),
                new KeyValuePair<string, string>("DynamicDataOptions:AutoMigrate", "true"),
                new KeyValuePair<string, string>("LiteDb:DbFileName", "test.ldb"),
                new KeyValuePair<string, string>("Dapper.Mssql:ConnectionString",
                    $"Data Source={dbServer};Initial Catalog={dbCatalog};Integrated Security=False;User ID=appfx;Password=0pTSyNY8iwxC20J7;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"),
                new KeyValuePair<string, string>("Dapper.Pgsql:ConnectionString",
                    $"User ID=appfx;Password=0pTSyNY8iwxC20J7;Host={dbServer};Port=5432;Database={dbCatalog};Pooling=true;"),
                new KeyValuePair<string, string>("Dapper.Mysql:ConnectionString",
                    $"Server={dbServer};Database={dbCatalog};Uid=appfx;Pwd=0pTSyNY8iwxC20J7")
            });

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();
            
            var manager = new ConsoleReportingConfigurationManager(config);
            var services = new ServiceCollection();
            services.ConfigureDynamicIdentityDataProvider(manager);

            var sp = services.BuildServiceProvider();

            var dataService = sp.GetRequiredService<IIdentityDataService>();

            using (var uow = dataService.BeginUnitOfWork())
            {
            }
        }
    }
}
