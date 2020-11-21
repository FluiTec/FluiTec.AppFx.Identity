using System;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user email store test.</summary>
    public abstract class UserEmailStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserEmailStore<UserEntity> GetStore => new UserStore(DataService);

        /// <summary>   (Unit Test Method) can get email asynchronous.</summary>
        [TestMethod]
        public void CanGetEmailAsync()
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

                Assert.AreEqual(user.Email, store.GetEmailAsync(user, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set email asynchronous.</summary>
        [TestMethod]
        public void CanSetEmailAsync()
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

                user.Phone = "test2@test.de";
                store.SetEmailAsync(user, "test2@test.de", CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(user.Email, dbEntity.Email);
            }
        }

        /// <summary>   (Unit Test Method) can get email confirmed asynchronous.</summary>
        [TestMethod]
        public void CanGetEmailConfirmedAsync()
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

                Assert.AreEqual(user.EmailConfirmed, store.GetEmailConfirmedAsync(user, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set email confirmed asynchronous.</summary>
        [TestMethod]
        public void CanSetEmailConfirmedAsync()
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

                user.EmailConfirmed = true;
                store.SetEmailConfirmedAsync(user, true, CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(user.EmailConfirmed, dbEntity.EmailConfirmed);
            }
        }

        /// <summary>   (Unit Test Method) can find by email asynchronous.</summary>
        [TestMethod]
        public void CanFindByEmailAsync()
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

                var dbEntity = store.FindByEmailAsync(user.Name, CancellationToken.None).Result;
                Assert.AreEqual(user.Email, dbEntity.Email);
            }
        }

        /// <summary>   (Unit Test Method) can get normalized email asynchronous.</summary>
        [TestMethod]
        public void CanGetNormalizedEmailAsync()
        {
            // nothing to test here - normalized names are ignored
        }

        /// <summary>   (Unit Test Method) can set normalized email asynchronous.</summary>
        [TestMethod]
        public void CanSetNormalizedEmailAsync()
        {
            // nothing to test here - normalized names are ignored
        }
    }
}