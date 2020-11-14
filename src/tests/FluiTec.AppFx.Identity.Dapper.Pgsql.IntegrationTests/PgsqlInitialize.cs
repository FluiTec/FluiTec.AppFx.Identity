using System;
using System.IO;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Identity.TestLibrary.Configuration;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Pgsql.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class PgsqlInitialize
    {
        internal static PgsqlDapperServiceOptions ServiceOptions;

        internal static PgsqlIdentityDataService DataService;

        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
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

                    var manager = new ConfigurationManager(config);
                    var options = manager.ExtractSettings<PgsqlAdminOptions>();
                    var pgsqlOptions = manager.ExtractSettings<PgsqlDapperServiceOptions>();

                    if (string.IsNullOrWhiteSpace(options.AdminConnectionString) ||
                        string.IsNullOrWhiteSpace(options.IntegrationDb) ||
                        string.IsNullOrWhiteSpace(options.IntegrationUser) ||
                        string.IsNullOrWhiteSpace(options.IntegrationPassword)) return;
                    if (string.IsNullOrWhiteSpace(pgsqlOptions.ConnectionString)) return;

                    PgsqlAdminHelper.CreateDababase(options.AdminConnectionString, options.IntegrationDb);
                    PgsqlAdminHelper.CreateUserAndLogin(options.AdminConnectionString, options.IntegrationDb,
                        options.IntegrationUser, options.IntegrationPassword);

                    ServiceOptions = new PgsqlDapperServiceOptions
                    {
                        ConnectionString = pgsqlOptions.ConnectionString
                    };
                    DataService = new PgsqlIdentityDataService(ServiceOptions, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}