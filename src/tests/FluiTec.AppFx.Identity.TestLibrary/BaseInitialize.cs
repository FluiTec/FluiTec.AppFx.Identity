using System;
using System.IO;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Identity.Dapper;
using FluiTec.AppFx.Options.Helpers;
using Microsoft.Extensions.Configuration;

namespace FluiTec.AppFx.Identity.TestLibrary
{
    /// <summary>   A base initialize.</summary>
    public static class BaseInitialize
    {
        /// <summary>   Gets integration configuration.</summary>
        /// <returns>   The integration configuration.</returns>
        public static IConfigurationRoot GetIntegrationConfiguration()
        {
            var path = DirectoryHelper.GetApplicationRoot();
            var parent = Directory.GetParent(path).Parent?.Parent?.FullName;
            var config = new ConfigurationBuilder()
                .SetBasePath(parent)
                .AddJsonFile("appsettings.integration.json", false, true)
                .AddJsonFile("appsettings.integration.secret.json", true, true)
                .Build();

            return config;
        }

        /// <summary>   Migrate up.</summary>
        /// <param name="serviceOptions">   Options for controlling the service. </param>
        /// <param name="dataService">      The data service. </param>
        public static void MigrateUp(IDapperServiceOptions serviceOptions, IDapperDataService dataService)
        {
            if (serviceOptions == null || dataService == null) return;
            
            var migrator = new DapperDataMigrator(serviceOptions.ConnectionString,
                new[] { typeof(DapperIdentityDataService).Assembly }, dataService.MetaData, builder => ConfigureSqlProvider(dataService, builder));
            migrator.Migrate();
        }

        /// <summary>   Configure SQL provider.</summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="builder">      The builder. </param>
        private static void ConfigureSqlProvider(IDapperDataService dataService, IMigrationRunnerBuilder builder)
        {
            switch (dataService.SqlType)
            {
                case SqlType.Mssql:
                    builder.AddSqlServer();
                    break;
                case SqlType.Mysql:
                    builder.AddMySql5();
                    break;
                case SqlType.Pgsql:
                    builder.AddPostgres();
                    break;
                case SqlType.Sqlite:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>   Migrate down.</summary>
        /// <param name="serviceOptions">   Options for controlling the service. </param>
        /// <param name="dataService">      The data service. </param>
        public static void MigrateDown(IDapperServiceOptions serviceOptions, IDapperDataService dataService)
        {
            if (serviceOptions == null || dataService == null) return;

            var migrator = new DapperDataMigrator(serviceOptions.ConnectionString,
                new[] { typeof(DapperIdentityDataService).Assembly }, dataService.MetaData,
                builder => builder.AddSqlServer());
            migrator.Migrate(default);
        }
    }
}