using System;
using System.Linq;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.DbTests
{
    /// <summary>   A database test.</summary>
    public abstract partial class DbTest
    {
        /// <summary>   (Unit Test Method) can create role.</summary>
        [TestMethod]
        public void CanCreateRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
            Assert.IsTrue(entity.Id != Guid.Empty);
        }

        /// <summary>   (Unit Test Method) can read role.</summary>
        [TestMethod]
        public void CanReadRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
            var dbEntity = uow.RoleRepository.Get(entity.Id);

            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can read role by identifier.</summary>
        [TestMethod]
        public void CanReadRoleByIdentifier()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
            var dbEntity = uow.RoleRepository.Get(entity.Id.ToString());

            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can find role by normalized name.</summary>
        [TestMethod]
        public void CanFindRoleByNormalizedName()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
            var dbEntity = uow.RoleRepository.FindByNormalizedName(entity.Name.ToUpper());

            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can find role by names.</summary>
        [TestMethod]
        public void CanFindRoleByNames()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
            var searchResult1 = uow.RoleRepository.FindByNames(new[] {entity.Name});
            var searchResult2 = uow.RoleRepository.FindByNames(new[] {entity.Name, "test"});

            Assert.IsTrue(searchResult1.Any(sr => sr.Id == entity.Id));
            Assert.IsTrue(searchResult2.Any(sr => sr.Id == entity.Id));
        }

        /// <summary>   (Unit Test Method) can find role by identifiers.</summary>
        [TestMethod]
        public void CanFindRoleByIds()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
            var searchResult1 = uow.RoleRepository.FindByIds(new[] {entity.Id});
            var searchResult2 = uow.RoleRepository.FindByIds(new[] {entity.Id, Guid.NewGuid()});

            Assert.IsTrue(searchResult1.Any(sr => sr.Id == entity.Id));
            Assert.IsTrue(searchResult2.Any(sr => sr.Id == entity.Id));
        }

        /// <summary>   (Unit Test Method) can update role.</summary>
        [TestMethod]
        public void CanUpdateRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});

            entity.Name = "TestRole2";
            uow.RoleRepository.Update(entity);

            var dbEntity = uow.RoleRepository.Get(entity.Id);
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can delete role.</summary>
        [TestMethod]
        public void CanDeleteRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
            entity.Name = "Test2";

            uow.RoleRepository.Delete(entity);
            var dbEntity = uow.RoleRepository.Get(entity.Id);
            Assert.IsNull(dbEntity);
        }
    }
}