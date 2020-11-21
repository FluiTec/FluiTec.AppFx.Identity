using System;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user lockout store test.</summary>
    public abstract class UserLockoutStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserLockoutStore<UserEntity> GetStore => new UserSecurityStore(DataService);

        /// <summary>   (Unit Test Method) can get lockout end date asynchronous.</summary>
        [TestMethod]
        public void CanGetLockoutEndDateAsync()
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

                Assert.AreEqual(user.LockedOutTill, store.GetLockoutEndDateAsync(user, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set lockout end date asynchronous.</summary>
        [TestMethod]
        public void CanSetLockoutEndDateAsync()
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

                user.LockedOutTill = DateTimeOffset.Now.AddHours(1);
                store.SetLockoutEndDateAsync(user, user.LockedOutTill, CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.IsNotNull(dbEntity.LockedOutTill);
                // can't compare exactly, since this would make mysql-tests fail, since mysql doesnt
                // offer the required precision upon storing
                Assert.AreEqual(user.LockedOutTill.Value.Year, dbEntity.LockedOutTill.Value.Year);
                Assert.AreEqual(user.LockedOutTill.Value.Month, dbEntity.LockedOutTill.Value.Month);
                Assert.AreEqual(user.LockedOutTill.Value.Day, dbEntity.LockedOutTill.Value.Day);
                Assert.AreEqual(user.LockedOutTill.Value.Hour, dbEntity.LockedOutTill.Value.Hour);
                Assert.AreEqual(user.LockedOutTill.Value.Minute, dbEntity.LockedOutTill.Value.Minute);
                Assert.AreEqual(user.LockedOutTill.Value.Second, dbEntity.LockedOutTill.Value.Second);
            }
        }

        /// <summary>   (Unit Test Method) can increment access failed count asynchronous.</summary>
        [TestMethod]
        public void CanIncrementAccessFailedCountAsync()
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

                user.AccessFailedCount += 1;
                store.IncrementAccessFailedCountAsync(user, CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(user.AccessFailedCount, dbEntity.AccessFailedCount);
            }
        }

        /// <summary>   (Unit Test Method) can reset access failed count asynchronous.</summary>
        [TestMethod]
        public void CanResetAccessFailedCountAsync()
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

                user.AccessFailedCount = 0;
                store.ResetAccessFailedCountAsync(user, CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(user.AccessFailedCount, dbEntity.AccessFailedCount);
            }
        }

        /// <summary>   (Unit Test Method) can get access failed count asynchronous.</summary>
        [TestMethod]
        public void CanGetAccessFailedCountAsync()
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

                Assert.AreEqual(user.AccessFailedCount, store.GetAccessFailedCountAsync(user, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can get lockout enabled asynchronous.</summary>
        [TestMethod]
        public void CanGetLockoutEnabledAsync()
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

                Assert.AreEqual(user.LockoutEnabled, store.GetLockoutEnabledAsync(user, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set lockout enabled asynchronous.</summary>
        [TestMethod]
        public void CanSetLockoutEnabledAsync()
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

                user.LockoutEnabled = true;
                store.SetLockoutEnabledAsync(user, true, CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(user.LockoutEnabled, dbEntity.LockoutEnabled);
            }
        }
    }
}