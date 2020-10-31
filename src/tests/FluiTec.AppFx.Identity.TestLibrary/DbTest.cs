using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Identity.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary
{
    /// <summary>   A database test.</summary>
    public abstract class DbTest
    {
        #region Fields

        /// <summary>   Gets a value indicating whether the database is available.</summary>
        /// <value> True if the database is available, false if not.</value>
        protected bool IsDbAvailable => ServiceOptions != null;

        #endregion

        #region Properties

        /// <summary>   Gets or sets options for controlling the service.</summary>
        /// <value> Options that control the service.</value>
        protected IDapperServiceOptions ServiceOptions { get; set; }

        /// <summary>   Gets or sets the data service.</summary>
        /// <value> The data service.</value>
        protected IIdentityDataService DataService { get; set; }

        #endregion

        #region Constructors

        /// <summary>   Specialized default constructor for use only by derived class.</summary>
        protected DbTest()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            InitOptionsAndDataService();
        }

        #endregion

        #region Methods

        /// <summary>   Initializes the options and data service.</summary>
        protected abstract void InitOptionsAndDataService();

        /// <summary>   Assert database available.</summary>
        protected void AssertDbAvailable()
        {
            Assert.IsTrue(IsDbAvailable, "DB NOT AVAILABLE!");
        }

        #endregion

        #region TestMethods

        /// <summary>   Can check apply migrations.</summary>
        /// <remarks>   You need to add [TestMethod] and [TestInitialize] when inheriting!</remarks>
        [TestMethod]
        [TestInitialize]
        public abstract void CanCheckApplyMigrations();

        /// <summary>   (Unit Test Method) can create unit of work.</summary>
        [TestMethod]
        public void CanCreateUnitOfWork()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
        }

        #endregion
    }
}