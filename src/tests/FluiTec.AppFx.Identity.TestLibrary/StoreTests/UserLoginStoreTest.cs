using System;
using System.Linq;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user login store test.</summary>
    public abstract class UserLoginStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserLoginStore<UserEntity> GetStore => new UserStore(DataService);

        /// <summary>   (Unit Test Method) can add login asynchronous.</summary>
        [TestMethod]
        public void CanAddLoginAsync()
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

                store.AddLoginAsync(user, new UserLoginInfo("providerXY", "providerXY", "providerXY"),
                    CancellationToken.None).Wait();
            }
        }

        /// <summary>   (Unit Test Method) can remove login asynchronous.</summary>
        [TestMethod]
        public void CanRemoveLoginAsync()
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

                store.AddLoginAsync(user, new UserLoginInfo("providerXY", "providerXY", "providerXY"),
                    CancellationToken.None).Wait();

                store.RemoveLoginAsync(user, "providerXY", "providerXY", CancellationToken.None).Wait();

                var logins = store.GetLoginsAsync(user, CancellationToken.None).Result;
                Assert.IsFalse(logins.Any(l => l.LoginProvider == "providerXY"));
            }
        }

        /// <summary>   (Unit Test Method) can get logins asynchronous.</summary>
        [TestMethod]
        public void CanGetLoginsAsync()
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

                store.AddLoginAsync(user, new UserLoginInfo("providerXY", "providerXY", "providerXY"),
                    CancellationToken.None).Wait();

                var logins = store.GetLoginsAsync(user, CancellationToken.None).Result;
                Assert.IsTrue(logins.Any(l => l.LoginProvider == "providerXY"));
            }
        }

        /// <summary>   (Unit Test Method) can find by login asynchronous.</summary>
        [TestMethod]
        public void CanFindByLoginAsync()
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

                store.AddLoginAsync(user, new UserLoginInfo("providerXY", "providerXY", "providerXY"),
                    CancellationToken.None).Wait();

                var dbEntity = store.FindByLoginAsync("providerXY", "providerXY", CancellationToken.None).Result;
                Assert.IsNotNull(dbEntity);
            }
        }
    }
}