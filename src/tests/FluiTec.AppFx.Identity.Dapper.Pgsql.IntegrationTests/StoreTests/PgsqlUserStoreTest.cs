﻿using FluiTec.AppFx.Identity.TestLibrary.StoreTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Dapper.Pgsql.IntegrationTests.StoreTests
{
    /// <summary>   (Unit Test Class) a pgsql store test. </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlUserStoreTest : UserStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a pgsql user phone number store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlUserPhoneNumberStoreTest : UserPhoneNumberStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a pgsql email store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlEmailStoreTest : UserEmailStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a pgsql password store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlPasswordStoreTest : UserPasswordStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a pgsql user claim store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlUserClaimStoreTest : UserClaimStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a pgsql user login store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlUserLoginStoreTest : UserLoginStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Pgsql user security stamp store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlUserSecurityStampStoreTest : UserSecurityStampStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Pgsql two factor store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTwoFactorStoreTest : UserTwoFactorStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Pgsql lockout store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlLockoutStoreTest : UserLockoutStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Pgsql role store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlRoleStoreTest : RoleStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }

    /// <summary>   (Unit Test Class) a Pgsql user role store test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlUserRoleStoreTest : UserRoleStoreTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = PgsqlInitialize.DataService;
            ServiceOptions = PgsqlInitialize.ServiceOptions;
        }
    }
}