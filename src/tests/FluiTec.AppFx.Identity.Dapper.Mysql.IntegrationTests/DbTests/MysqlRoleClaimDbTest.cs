using FluiTec.AppFx.Identity.TestLibrary.DbTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mysql.IntegrationTests.DbTests
{
    /// <summary>   (Unit Test Class) a Mysql test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlRoleClaimDbTest : RoleClaimDbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }
}