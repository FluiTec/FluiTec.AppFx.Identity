using FluiTec.AppFx.Identity.TestLibrary.DbTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a mssql test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlDbTest : DbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }
}