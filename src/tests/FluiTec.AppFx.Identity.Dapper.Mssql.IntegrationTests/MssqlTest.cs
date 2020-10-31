using System;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Identity.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a mssql test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlTest : DbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            var pw = Environment.GetEnvironmentVariable("SA_PASSWORD");

            if (string.IsNullOrWhiteSpace(pw)) return;

            ServiceOptions = new MssqlDapperServiceOptions
            {
                ConnectionString = $"Data Source=microsoft-mssql-server-linux;Initial Catalog=master;Integrated Security=False;User ID=sa;Password={pw};Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            };

            DataService = new MssqlIdentityDataService(ServiceOptions, null);
        }

        /// <summary>   (Unit Test Method) can check apply migrations.</summary>
        [TestInitialize]
        public override void CanCheckApplyMigrations()
        {
            AssertDbAvailable();

            var migrator = new DapperDataMigrator(ServiceOptions.ConnectionString, new[] { typeof(DapperIdentityDataService).Assembly }, ((IDapperDataService)DataService).MetaData,
                builder => builder.AddSqlServer());
            migrator.Migrate();
        }
    }
}