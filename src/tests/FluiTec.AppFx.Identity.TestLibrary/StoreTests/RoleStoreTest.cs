using System;
using System.Threading;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.EntityStores;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.StoreTests
{
    /// <summary>   A role store test.</summary>
    public abstract class RoleStoreTest : StoreTest
    {
        /// <summary>   Gets the get store.</summary>
        /// <value> The get store.</value>
        private IRoleStore<RoleEntity> GetStore => new RoleStore(DataService);

        /// <summary>   (Unit Test Method) can create asynchronous.</summary>
        [TestMethod]
        public void CanCreateAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                var createResult = store.CreateAsync(role, CancellationToken.None).Result;

                Assert.AreEqual(IdentityResult.Success, createResult);
            }
        }

        /// <summary>   (Unit Test Method) can update asynchronous.</summary>
        [TestMethod]
        public void CanUpdateAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                var createResult = store.CreateAsync(role, CancellationToken.None).Result;

                role.Name = "TestRole2";
                var updateResult = store.UpdateAsync(role, CancellationToken.None).Result;

                var dbEntity = store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(role.Name, dbEntity.Name);
            }
        }

        /// <summary>   (Unit Test Method) can delete asynchronous.</summary>
        [TestMethod]
        public void CanDeleteAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                var createResult = store.CreateAsync(role, CancellationToken.None).Result;

                var deleteResult = store.DeleteAsync(role, CancellationToken.None).Result;

                var dbEntity = store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(null, dbEntity);
            }
        }

        /// <summary>   (Unit Test Method) can find by identifier asynchronous.</summary>
        [TestMethod]
        public void CanFindByIdAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                var createResult = store.CreateAsync(role, CancellationToken.None).Result;

                var dbEntity = store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(role.Name, dbEntity.Name);
            }
        }

        /// <summary>   (Unit Test Method) can find by name asynchronous.</summary>
        [TestMethod]
        public void CanFindByNameAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                var createResult = store.CreateAsync(role, CancellationToken.None).Result;

                var dbEntity = store.FindByNameAsync(role.Name, CancellationToken.None).Result;
                Assert.AreEqual(role.Name, dbEntity.Name);
            }
        }

        /// <summary>   (Unit Test Method) can get role identifier asynchronous.</summary>
        [TestMethod]
        public void CanGetRoleIdAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };

                Assert.AreEqual(role.Id.ToString(), store.GetRoleIdAsync(role, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can get role name asynchronous.</summary>
        [TestMethod]
        public void CanGetRoleNameAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };

                Assert.AreEqual(role.Name, store.GetRoleNameAsync(role, CancellationToken.None).Result);
            }
        }

        /// <summary>   (Unit Test Method) can set role name asynchronous.</summary>
        [TestMethod]
        public void CanSetRoleNameAsync()
        {
            using (var store = GetStore)
            {
                var role = new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRole"
                };
                var createResult = store.CreateAsync(role, CancellationToken.None).Result;

                role.Name = "TestRole2";
                store.SetRoleNameAsync(role, "TestRole2", CancellationToken.None).Wait();

                var dbEntity = store.FindByIdAsync(role.Id.ToString(), CancellationToken.None).Result;
                Assert.AreEqual(role.Name, dbEntity.Name);
            }
        }

        /// <summary>   (Unit Test Method) can get normalized role name asynchronous.</summary>
        [TestMethod]
        public void CanGetNormalizedRoleNameAsync()
        {
            // nothing to test here - normalized names are ignored
        }

        /// <summary>   (Unit Test Method) can set normalized role name asynchronous.</summary>
        [TestMethod]
        public void CanSetNormalizedRoleNameAsync()
        {
            // nothing to test here - normalized names are ignored
        }
    }
}