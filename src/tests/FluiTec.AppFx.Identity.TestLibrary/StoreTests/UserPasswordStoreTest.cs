using System;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user password store test.</summary>
    public abstract class UserPasswordStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserPasswordStore<UserEntity> GetStore => new UserStore(DataService);

        /// <summary>   (Unit Test Method) can get password hash asynchronous.</summary>
        [TestMethod]
        public void CanGetPasswordHashAsync()
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

                Assert.AreEqual(user.PasswordHash, store.GetPasswordHashAsync(user, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set password hash asynchronous.</summary>
        [TestMethod]
        public void CanSetPasswordHashAsync()
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

                user.PasswordHash = "abc";
                store.SetPasswordHashAsync(user, "abc", CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(user.PasswordHash, dbEntity.PasswordHash);
            }
        }

        /// <summary>   (Unit Test Method) can has password asynchronous.</summary>
        [TestMethod]
        public void CanHasPasswordAsync()
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

                Assert.AreEqual(false, store.HasPasswordAsync(user, CancellationToken.None).Result);
            }
        }
    }
}