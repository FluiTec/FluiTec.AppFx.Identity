using System;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user store test.</summary>
    public abstract class UserStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserStore<UserEntity> GetStore => new UserStore(DataService);

        /// <summary>   (Unit Test Method) can create asynchronous.</summary>
        [TestMethod]
        public void CanCreateAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                Assert.AreEqual(IdentityResult.Success, createResult);
            }
        }

        /// <summary>   (Unit Test Method) can find by identifier asynchronous.</summary>
        [TestMethod]
        public void CanFindByIdAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), new CancellationToken()).Result;
                Assert.AreEqual(user.Email, dbEntity.Email);
            }
        }

        /// <summary>   (Unit Test Method) can find by name asynchronous.</summary>
        [TestMethod]
        public void CanFindByNameAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                var dbEntity = store.FindByNameAsync(user.Name, new CancellationToken()).Result;
                Assert.AreEqual(user.Email, dbEntity.Email);
            }
        }

        /// <summary>   (Unit Test Method) can update asynchronous.</summary>
        [TestMethod]
        public void CanUpdateAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                user.Name = "test2@test.de";
                var updateResult = store.UpdateAsync(user, new CancellationToken()).Result;

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), new CancellationToken()).Result;
                Assert.AreEqual(user.Name, dbEntity.Name);
            }
        }

        /// <summary>   (Unit Test Method) can delete asynchronous.</summary>
        [TestMethod]
        public void CanDeleteAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                var updateResult = store.DeleteAsync(user, new CancellationToken()).Result;

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), new CancellationToken()).Result;
                Assert.AreEqual(null, dbEntity);
            }
        }

        /// <summary>   (Unit Test Method) can get user identifier asynchronous.</summary>
        [TestMethod]
        public void CanGetUserIdAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                Assert.AreEqual(user.Id.ToString(), store.GetUserIdAsync(user, new CancellationToken()).Result);
            }
        }

        /// <summary>   (Unit Test Method) can get user name asynchronous.</summary>
        [TestMethod]
        public void CanGetUserNameAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                Assert.AreEqual(user.Name, store.GetUserNameAsync(user, new CancellationToken()).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set user name asynchronous.</summary>
        [TestMethod]
        public void CanSetUserNameAsync()
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
                var createResult = store.CreateAsync(user, new CancellationToken()).Result;

                store.SetUserNameAsync(user, "test2@test.de", new CancellationToken()).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), new CancellationToken()).Result;
                Assert.AreEqual(user.Name, dbEntity.Name);
            }
        }

        /// <summary>   (Unit Test Method) can get normalized user name asynchronous.</summary>
        [TestMethod]
        public void CanGetNormalizedUserNameAsync()
        {
            using (var store = GetStore)
            {
                // nothing to test here - normalized names are ignored
            }
        }

        /// <summary>   (Unit Test Method) can set normalized user name asynchronous.</summary>
        [TestMethod]
        public void CanSetNormalizedUserNameAsync()
        {
            using (var store = GetStore)
            {
                // nothing to test here - normalized names are ignored
            }
        }
    }
}
