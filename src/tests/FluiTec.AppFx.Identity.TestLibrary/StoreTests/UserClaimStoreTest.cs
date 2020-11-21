using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user claim store test.</summary>
    public abstract class UserClaimStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IUserClaimStore<UserEntity> GetStore => new UserStore(DataService);

        /// <summary>   (Unit Test Method) can get claims asynchronous.</summary>
        [TestMethod]
        public void CanGetClaimsAsync()
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

                store.AddClaimsAsync(user, new[] { new Claim("uClaimType", "uClaimValue") }, CancellationToken.None).Wait();

                var claims = store.GetClaimsAsync(user, CancellationToken.None).Result;

                Assert.IsTrue(claims.Any(c => c.Value == "uClaimValue"));
            }
        }

        /// <summary>   (Unit Test Method) can add claims asynchronous.</summary>
        [TestMethod]
        public void CanAddClaimsAsync()
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

                store.AddClaimsAsync(user, new[] {new Claim("uClaimType", "uClaimValue")}, CancellationToken.None).Wait();
            }
        }

        /// <summary>   (Unit Test Method) can replace claim asynchronous.</summary>
        [TestMethod]
        public void CanReplaceClaimAsync()
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

                var c1 = new Claim("uClaimType1", "uClaimValue1");
                var c2 = new Claim("uClaimType2", "uClaimValue2");

                store.AddClaimsAsync(user, new[] { c1 }, CancellationToken.None).Wait();
                store.ReplaceClaimAsync(user, c1, c2, CancellationToken.None).Wait();

                var claims = store.GetClaimsAsync(user, CancellationToken.None).Result;
                Assert.IsTrue(claims.Any(c => c.Value == "uClaimValue2"));
                Assert.IsFalse(claims.Any(c => c.Value == "uClaimValue1"));
            }
        }

        /// <summary>   (Unit Test Method) can remove claims asynchronous.</summary>
        [TestMethod]
        public void CanRemoveClaimsAsync()
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

                var c1 = new Claim("uClaimType1", "uClaimValue1");
                var c2 = new Claim("uClaimType2", "uClaimValue2");

                store.AddClaimsAsync(user, new[] { c1, c2 }, CancellationToken.None).Wait();
                store.RemoveClaimsAsync(user, new[] {c1, c2}, CancellationToken.None).Wait();

                var claims = store.GetClaimsAsync(user, CancellationToken.None).Result;
                Assert.IsFalse(claims.Any(c => c.Value == "uClaimValue1"));
                Assert.IsFalse(claims.Any(c => c.Value == "uClaimValue2"));
            }
        }

        /// <summary>   (Unit Test Method) can get users for claim asynchronous.</summary>
        [TestMethod]
        public void CanGetUsersForClaimAsync()
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

                var c1 = new Claim("uClaimType1", "uClaimValue1");
                var c2 = new Claim("uClaimType2", "uClaimValue2");

                store.AddClaimsAsync(user, new[] { c1, c2 }, CancellationToken.None).Wait();

                var users = store.GetUsersForClaimAsync(c1, CancellationToken.None).Result;
                Assert.IsTrue(users.Any(u => u.Id == user.Id));
            }
        }
    }
}