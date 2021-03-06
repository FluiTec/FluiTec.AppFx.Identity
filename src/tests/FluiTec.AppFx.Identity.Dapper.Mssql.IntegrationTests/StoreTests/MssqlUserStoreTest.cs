﻿using FluiTec.AppFx.Identity.TestLibrary.StoreTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.IntegrationTests.StoreTests
{
    /// <summary>   (Unit Test Class) a mssql store test. </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserStoreTest : UserStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql user phone number store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserPhoneNumberStoreTest : UserPhoneNumberStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql user email store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserEmailStoreTest : UserEmailStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql password store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlPasswordStoreTest : UserPasswordStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql user claim store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserClaimStoreTest : UserClaimStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql user login store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserLoginStoreTest : UserLoginStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql user security stamp store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserSecurityStampStoreTest : UserSecurityStampStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql two factor store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlTwoFactorStoreTest : UserTwoFactorStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql lockout store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlLockoutStoreTest : UserLockoutStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql role store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlRoleStoreTest : RoleStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a mssql user role store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlUserRoleStoreTest : UserRoleStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = MssqlInitialize.DataService;
            ServiceOptions = MssqlInitialize.ServiceOptions;
        }
    }
}