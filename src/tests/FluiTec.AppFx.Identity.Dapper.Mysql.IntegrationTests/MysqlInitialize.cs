using System;
using System.IO;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Identity.TestLibrary;
using FluiTec.AppFx.Identity.TestLibrary.Configuration;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mysql.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class MysqlInitialize
    {
        internal static MysqlDapperServiceOptions ServiceOptions;

        internal static MysqlIdentityDataService DataService;

        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var pw = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");

            if (!string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(pw))
            {
                ServiceOptions = new MysqlDapperServiceOptions
                {
                    ConnectionString = $"Server=mysql;Database={db};Uid=root;Pwd={pw}"
                };

                DataService = new MysqlIdentityDataService(ServiceOptions, null);
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
                    var options = manager.ExtractSettings<MysqlAdminOption>();
                    var mysqlOptions = manager.ExtractSettings<MysqlDapperServiceOptions>();

                    if (string.IsNullOrWhiteSpace(options.AdminConnectionString) ||
                        string.IsNullOrWhiteSpace(options.IntegrationDb) ||
                        string.IsNullOrWhiteSpace(options.IntegrationUser) ||
                        string.IsNullOrWhiteSpace(options.IntegrationPassword)) return;
                    if (string.IsNullOrWhiteSpace(mysqlOptions.ConnectionString)) return;

                    MysqlAdminHelper.CreateDababase(options.AdminConnectionString, options.IntegrationDb);
                    MysqlAdminHelper.CreateUserAndLogin(options.AdminConnectionString, options.IntegrationDb,
                        options.IntegrationUser, options.IntegrationPassword);

                    ServiceOptions = new MysqlDapperServiceOptions
                    {
                        ConnectionString = mysqlOptions.ConnectionString
                    };
                    DataService = new MysqlIdentityDataService(ServiceOptions, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            BaseInitialize.MigrateUp(ServiceOptions, DataService);
        }
    }
}