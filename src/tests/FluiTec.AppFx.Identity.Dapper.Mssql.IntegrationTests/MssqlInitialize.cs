using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Identity.TestLibrary;
using FluiTec.AppFx.Identity.TestLibrary.Configuration;
using FluiTec.AppFx.Options.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class MssqlInitialize
    {
        internal static MssqlDapperServiceOptions ServiceOptions;

        internal static MssqlIdentityDataService DataService;

        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var pw = Environment.GetEnvironmentVariable("SA_PASSWORD");

            if (!string.IsNullOrWhiteSpace(pw))
            {
                ServiceOptions = new MssqlDapperServiceOptions
                {
                    ConnectionString =
                        $"Data Source=microsoft-mssql-server-linux;Initial Catalog=master;Integrated Security=False;User ID=sa;Password={pw};Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                };

                DataService = new MssqlIdentityDataService(ServiceOptions, null);
            }
            else
            {
                try
                {
                    var config = BaseInitialize.GetIntegrationConfiguration();

                    var manager = new ConfigurationManager(config);
                    var options = manager.ExtractSettings<MssqlAdminOption>();
                    var mssqlOptions = manager.ExtractSettings<MssqlDapperServiceOptions>();

                    if (string.IsNullOrWhiteSpace(options.AdminConnectionString) ||
                        string.IsNullOrWhiteSpace(options.IntegrationDb) ||
                        string.IsNullOrWhiteSpace(options.IntegrationUser) ||
                        string.IsNullOrWhiteSpace(options.IntegrationPassword)) return;
                    if (string.IsNullOrWhiteSpace(mssqlOptions.ConnectionString)) return;

                    MssqlAdminHelper.CreateDababase(options.AdminConnectionString, options.IntegrationDb);
                    MssqlAdminHelper.CreateUserAndLogin(options.AdminConnectionString, options.IntegrationDb,
                        options.IntegrationUser, options.IntegrationPassword);

                    ServiceOptions = new MssqlDapperServiceOptions
                    {
                        ConnectionString = mssqlOptions.ConnectionString
                    };
                    DataService = new MssqlIdentityDataService(ServiceOptions, null);
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