using System;
using System.IO;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Identity.Dapper.Mssql;
using FluiTec.AppFx.Identity.TestLibrary;
using FluiTec.AppFx.Options.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mysql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a mysql test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlTest : DbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
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

                    var manager = new Options.Managers.ConfigurationManager(config);
                    var mysqlOptions = manager.ExtractSettings<MysqlDapperServiceOptions>();

                    ServiceOptions = new MysqlDapperServiceOptions
                    {
                        ConnectionString = mysqlOptions.ConnectionString
                    };
                    DataService = new MysqlIdentityDataService(ServiceOptions, null);
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }
    }
}