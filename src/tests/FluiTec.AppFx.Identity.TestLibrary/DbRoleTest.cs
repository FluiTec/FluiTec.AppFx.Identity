using System;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary
{
    /// <summary>   A database test.</summary>
    public abstract partial class DbTest
    {
        /// <summary>   (Unit Test Method) can create role.</summary>
        [TestMethod]
        public void CanCreateRole()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity {Name = "TestRole"});
            Assert.IsTrue(entity.Id != Guid.Empty);
        }

        /// <summary>   (Unit Test Method) can read role.</summary>
        [TestMethod]
        public void CanReadRole()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity { Name = "TestRole" });
            var dbEntity = uow.RoleRepository.Get(entity.Id);

            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can update role.</summary>
        [TestMethod]
        public void CanUpdateRole()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity { Name = "TestRole" });
            entity.Name = "TestRole2";

            uow.RoleRepository.Update(entity);

            var dbEntity = uow.RoleRepository.Get(entity.Id);
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can delete role.</summary>
        [TestMethod]
        public void CanDeleteRole()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.RoleRepository.Add(new RoleEntity { Name = "TestRole" });
            entity.Name = "Test2";

            uow.RoleRepository.Delete(entity);

            var dbEntity = uow.RoleRepository.Get(entity.Id);

            Assert.IsNull(dbEntity);
        }
    }
}