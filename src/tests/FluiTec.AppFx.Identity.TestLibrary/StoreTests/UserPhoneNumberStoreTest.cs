using System;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user phone number store test.</summary>
    public abstract class UserPhoneNumberStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserPhoneNumberStore<UserEntity> GetStore => new UserStore(DataService);

        /// <summary>   (Unit Test Method) can get phone number asynchronous.</summary>
        [TestMethod]
        public void CanGetPhoneNumberAsync()
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
                
                Assert.AreEqual(user.Phone, store.GetPhoneNumberAsync(user, new CancellationToken()).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set phone number asynchronous.</summary>
        [TestMethod]
        public void CanSetPhoneNumberAsync()
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

                user.Phone = "abc";
                store.SetPhoneNumberAsync(user, "abc", new CancellationToken()).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), new CancellationToken()).Result;
                Assert.AreEqual(user.Phone, dbEntity.Phone);
            }
        }

        /// <summary>   (Unit Test Method) can get phone number confirmed asynchronous.</summary>
        [TestMethod]
        public void CanGetPhoneNumberConfirmedAsync()
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

                Assert.AreEqual(user.PhoneConfirmed, store.GetPhoneNumberConfirmedAsync(user, new CancellationToken()).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set phone number confirmed asynchronous.</summary>
        [TestMethod]
        public void CanSetPhoneNumberConfirmedAsync()
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

                user.PhoneConfirmed = true;
                store.SetPhoneNumberConfirmedAsync(user, true, new CancellationToken()).Wait();

                var dbEntity = store.FindByIdAsync(user.Id.ToString(), new CancellationToken()).Result;
                Assert.AreEqual(user.PhoneConfirmed, dbEntity.PhoneConfirmed);
            }
        }
    }
}