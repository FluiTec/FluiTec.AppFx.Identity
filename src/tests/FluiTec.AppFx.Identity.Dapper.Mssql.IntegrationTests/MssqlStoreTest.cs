using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Identity.TestLibrary;
using FluiTec.AppFx.Identity.TestLibrary.StoreTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a mssql store test. </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlStoreTest : StoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }

        /// <summary>   Tests start.</summary>
        [TestInitialize]
        public void TestStart()
        {
            BaseInitialize.MigrateUp(ServiceOptions, (IDapperDataService)DataService);
        }

        /// <summary>   Tests stop.</summary>
        [TestCleanup]
        public void TestStop()
        {
            BaseInitialize.MigrateDown(ServiceOptions, (IDapperDataService)DataService);
        }
    }
}