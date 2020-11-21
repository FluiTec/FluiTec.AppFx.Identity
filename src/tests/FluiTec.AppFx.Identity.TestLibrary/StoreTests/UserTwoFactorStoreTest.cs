using System;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user two factor store test.</summary>
    public abstract class UserTwoFactorStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserTwoFactorStore<UserEntity> GetStore => new UserSecurityStore(DataService);

        /// <summary>   (Unit Test Method) can get two factor enabled asynchronous.</summary>
        [TestMethod]
        public void CanGetTwoFactorEnabledAsync()
        {
            using (var store = GetStore)
            {
                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "test@test.de",
                    Phone = "+49(0000)1111111",
                    PhoneConfirmed = false,
                    Email = "test@test.de",
                    EmailConfirmed = false,
                    FullName = "Max Mustermann",
                    PasswordHash = string.Empty,
                    SecurityStamp = string.Empty,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    LockedOutPermanently = false
                };
                var createResult = store.CreateAsync(user, CancellationToken.None).Result;

                Assert.AreEqual(user.TwoFactorEnabled, store.GetTwoFactorEnabledAsync(user, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set two factor enabled asynchronous.</summary>
        [TestMethod]
        public void CanSetTwoFactorEnabledAsync()
        {
            using (var store = GetStore)
            {
                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "test@test.de",
                    Phone = "+49(0000)1111111",
                    PhoneConfirmed = false,
                    Email = "test@test.de",
                    EmailConfirmed = false,
                    FullName = "Max Mustermann",
                    PasswordHash = string.Empty,
                    SecurityStamp = string.Empty,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    LockedOutPermanently = false
                };
                var createResult = store.CreateAsync(user, CancellationToken.None).Result;

                user.TwoFactorEnabled = true;
                store.SetTwoFactorEnabledAsync(user, true, CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(user.TwoFactorEnabled, dbEntity.TwoFactorEnabled);
            }
        }
    }
}