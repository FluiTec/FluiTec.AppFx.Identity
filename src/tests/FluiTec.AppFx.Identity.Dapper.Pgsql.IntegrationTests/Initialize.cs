﻿using System;
using System.IO;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Identity.TestLibrary.Configuration;
using FluiTec.AppFx.Options.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Pgsql.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var usr = Environment.GetEnvironmentVariable("POSTGRES_USER");

            PgsqlDapperServiceOptions serviceOptions = null;
            PgsqlIdentityDataService dataService = null;

            if (!string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(usr))
            {
                serviceOptions = new PgsqlDapperServiceOptions
                {
                    ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;"
                };

                dataService = new PgsqlIdentityDataService(serviceOptions, null);
            }
            else
            {
                try
                {
                    var path = DirectoryHelper.GetApplicationRoot();
                    var parent = Directory.GetParent(path).Parent?.Parent?.FullName;
                    var config = new ConfigurationBuilder()
                        .SetBasePath(parent)
                        .AddJsonFile("appsettings.integration.json", false, true)
                        .AddJsonFile("appsettings.integration.secret.json", true, true)
                        .Build();

                    var manager = new Options.Managers.ConfigurationManager(config);
                    var options = manager.ExtractSettings<PgsqlAdminOptions>();
                    var pgsqlOptions = manager.ExtractSettings<PgsqlDapperServiceOptions>();

                    if (string.IsNullOrWhiteSpace(options.AdminConnectionString) ||
                        string.IsNullOrWhiteSpace(options.IntegrationDb) ||
                        string.IsNullOrWhiteSpace(options.IntegrationUser) ||
                        string.IsNullOrWhiteSpace(options.IntegrationPassword)) return;
                    if (string.IsNullOrWhiteSpace(pgsqlOptions.ConnectionString)) return;

                    PgsqlAdminHelper.CreateDababase(options.AdminConnectionString, options.IntegrationDb);
                    PgsqlAdminHelper.CreateUserAndLogin(options.AdminConnectionString, options.IntegrationDb, options.IntegrationUser, options.IntegrationPassword);

                    serviceOptions = new PgsqlDapperServiceOptions
                    {
                        ConnectionString = pgsqlOptions.ConnectionString
                    };
                    dataService = new PgsqlIdentityDataService(serviceOptions, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            if (serviceOptions == null || dataService == null) return;

            var migrator = new DapperDataMigrator(serviceOptions.ConnectionString,
                new[] { (typeof(DapperIdentityDataService)).Assembly }, ((IDapperDataService)dataService).MetaData,
                builder => builder.AddPostgres());
            migrator.Migrate();
        }
    }
}
