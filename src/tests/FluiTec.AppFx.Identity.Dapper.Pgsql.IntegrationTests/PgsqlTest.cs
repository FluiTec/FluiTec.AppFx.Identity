﻿using System;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Identity.TestLibrary;
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

            if (string.IsNullOrWhiteSpace(db) || string.IsNullOrWhiteSpace(usr)) return;

            ServiceOptions = new PgsqlDapperServiceOptions
            {
                ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;"
            };

            DataService = new PgsqlIdentityDataService(ServiceOptions, null);
        }

        /// <summary>   (Unit Test Method) can check apply migrations.</summary>
        [TestInitialize]
        public override void CanCheckApplyMigrations()
        {
            AssertDbAvailable();

            var migrator = new DapperDataMigrator(ServiceOptions.ConnectionString, new[] { typeof(DapperIdentityDataService).Assembly }, ((IDapperDataService)DataService).MetaData,
                builder => builder.AddPostgres());
            migrator.Migrate();
        }
    }
}