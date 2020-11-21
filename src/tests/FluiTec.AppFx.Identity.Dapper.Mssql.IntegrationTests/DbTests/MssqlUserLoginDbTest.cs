using FluiTec.AppFx.Identity.TestLibrary.DbTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.IntegrationTests.DbTests
{
    /// <summary>   (Unit Test Class) a mssql user login database test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserLoginDbTest : UserLoginDbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }
}