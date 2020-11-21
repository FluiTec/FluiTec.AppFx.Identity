using FluiTec.AppFx.Identity.TestLibrary.DbTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.IntegrationTests.DbTests
{
    /// <summary>   (Unit Test Class) a mssql user claim database test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserClaimDbTest : UserClaimDbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }
}