using FluiTec.AppFx.Identity.TestLibrary.DbTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Pgsql.IntegrationTests.DbTests
{
    /// <summary>   (Unit Test Class) a Pgsql user role database test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlUserRoleDbTest : UserRoleDbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }
}