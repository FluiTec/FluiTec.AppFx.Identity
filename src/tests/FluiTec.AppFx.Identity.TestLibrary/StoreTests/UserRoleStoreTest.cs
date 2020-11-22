using System;
using System.Linq;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A user role store test.</summary>
    public abstract class UserRoleStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private RoleStore GetStore => new RoleStore(DataService);

        /// <summary>   (Unit Test Method) can add to role asynchronous.</summary>
        [TestMethod]
        public void CanAddToRoleAsync()
        {
            using (var store = GetStore)
            {
                var user1 = new UserEntity
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
                store.CreateAsync(user1, CancellationToken.None).Wait();

                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                store.CreateAsync(role, CancellationToken.None).Wait();

                store.AddToRoleAsync(user1, role.Name, CancellationToken.None).Wait();

                Assert.IsTrue(store.IsInRoleAsync(user1, role.Name, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can remove from role asynchronous.</summary>
        [TestMethod]
        public void CanRemoveFromRoleAsync()
        {
            using (var store = GetStore)
            {
                var user1 = new UserEntity
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
                store.CreateAsync(user1, CancellationToken.None).Wait();

                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                store.CreateAsync(role, CancellationToken.None).Wait();

                store.AddToRoleAsync(user1, role.Name, CancellationToken.None).Wait();
                store.RemoveFromRoleAsync(user1, role.Name, CancellationToken.None).Wait();

                Assert.IsFalse(store.IsInRoleAsync(user1, role.Name, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can get roles asynchronous.</summary>
        [TestMethod]
        public void CanGetRolesAsync()
        {
            using (var store = GetStore)
            {
                var user1 = new UserEntity
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
                store.CreateAsync(user1, CancellationToken.None).Wait();

                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                store.CreateAsync(role, CancellationToken.None).Wait();

                store.AddToRoleAsync(user1, role.Name, CancellationToken.None).Wait();
                Assert.IsTrue(store.GetRolesAsync(user1, CancellationToken.None).Result.Contains(role.Name));
            }
        }

        /// <summary>   (Unit Test Method) can is in role asynchronous.</summary>
        [TestMethod]
        public void CanIsInRoleAsync()
        {
            using (var store = GetStore)
            {
                var user1 = new UserEntity
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
                store.CreateAsync(user1, CancellationToken.None).Wait();

                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                store.CreateAsync(role, CancellationToken.None).Wait();

                store.AddToRoleAsync(user1, role.Name, CancellationToken.None).Wait();

                Assert.IsTrue(store.IsInRoleAsync(user1, role.Name, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can get users in role asynchronous.</summary>
        [TestMethod]
        public void CanGetUsersInRoleAsync()
        {
            using (var store = GetStore)
            {
                var user1 = new UserEntity
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
                store.CreateAsync(user1, CancellationToken.None).Wait();

                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                store.CreateAsync(role, CancellationToken.None).Wait();

                store.AddToRoleAsync(user1, role.Name, CancellationToken.None).Wait();

                var usersInRole = store.GetUsersInRoleAsync(role.Name, CancellationToken.None).Result;
                Assert.IsTrue(usersInRole.Any(u => u.Id == user1.Id));
            }
        }
    }
}