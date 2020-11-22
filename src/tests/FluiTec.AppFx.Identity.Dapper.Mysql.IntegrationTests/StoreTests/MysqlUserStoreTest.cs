using FluiTec.AppFx.Identity.TestLibrary.StoreTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mysql.IntegrationTests.StoreTests
{
    /// <summary>   (Unit Test Class) a mysql store test. </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlUserStoreTest : UserStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mysql user phone number store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlUserPhoneNumberStoreTest : UserPhoneNumberStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mysql email store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlEmailStoreTest : UserEmailStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mysql password store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlPasswordStoreTest : UserPasswordStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mysql user claim store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlUserClaimStoreTest : UserClaimStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mysql user login store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlUserLoginStoreTest : UserLoginStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Mysql user security stamp store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlUserSecurityStampStoreTest : UserSecurityStampStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Mysql two factor store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlTwoFactorStoreTest : UserTwoFactorStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Mysql lockout store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlLockoutStoreTest : UserLockoutStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Mysql role store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlRoleStoreTest : RoleStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Mysql user role store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlUserRoleStoreTest : UserRoleStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MysqlInitialize.DataService;
            ServiceOptions = MysqlInitialize.ServiceOptions;
        }
    }
}