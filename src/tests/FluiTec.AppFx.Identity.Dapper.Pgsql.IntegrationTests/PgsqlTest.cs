using System;
using System.IO;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Identity.Dapper.Mysql;
using FluiTec.AppFx.Identity.TestLibrary;
using FluiTec.AppFx.Options.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Pgsql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a pgsql test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTest : DbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var usr = Environment.GetEnvironmentVariable("POSTGRES_USER");

            if (!string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(usr))
            {
                ServiceOptions = new PgsqlDapperServiceOptions
                {
                    ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;"
                };

                DataService = new PgsqlIdentityDataService(ServiceOptions, null);
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
                    var pgsqlOptions = manager.ExtractSettings<PgsqlDapperServiceOptions>();

                    ServiceOptions = new PgsqlDapperServiceOptions
                    {
                        ConnectionString = pgsqlOptions.ConnectionString
                    };
                    DataService = new PgsqlIdentityDataService(ServiceOptions, null);
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }
    }
}