using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Identity.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A store test. </summary>
    public abstract class StoreTest
    {
        #region Constructors

        /// <summary>   Specialized default constructor for use only by derived class.</summary>
        protected StoreTest()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            InitOptionsAndDataService();
        }

        #endregion

        #region Fields

        /// <summary>   Gets a value indicating whether the database is available.</summary>
        /// <value> True if the database is available, false if not.</value>
        protected bool IsDbAvailable => ServiceOptions != null;

        #endregion

        #region TestMethods

        /// <summary>   (Unit Test Method) can create unit of work.</summary>
        [TestMethod]
        public void CanCreateUnitOfWork()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
        }

        /// <summary>   Clean database.</summary>
        [TestCleanup]
        public void CleanDatabase()
        {
            CleanUserLogins();
            CleanUserClaims();
            CleanUsers();
        }

        /// <summary>   Clean users.</summary>
        private void CleanUsers()
        {
            using (var uow = DataService.BeginUnitOfWork())
            {
                foreach (var entity in uow.UserRepository.GetAll())
                {
                    uow.UserRepository.Delete(entity);
                }
                uow.Commit();
            }
        }

        /// <summary>   Clean user claims.</summary>
        private void CleanUserClaims()
        {
            using (var uow = DataService.BeginUnitOfWork())
            {
                foreach (var entity in uow.UserClaimRepository.GetAll())
                {
                    uow.UserClaimRepository.Delete(entity);
                }
                uow.Commit();
            }
        }

        /// <summary>   Clean user logins.</summary>
        private void CleanUserLogins()
        {
            using (var uow = DataService.BeginUnitOfWork())
            {
                foreach (var entity in uow.LoginRepository.GetAll())
                {
                    uow.LoginRepository.Delete(entity);
                }
                uow.Commit();
            }
        }

        #endregion

        #region Properties

        /// <summary>   Gets or sets options for controlling the service.</summary>
        /// <value> Options that control the service.</value>
        protected IDapperServiceOptions ServiceOptions { get; set; }

        /// <summary>   Gets or sets the data service.</summary>
        /// <value> The data service.</value>
        protected IIdentityDataService DataService { get; set; }

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
    }
}