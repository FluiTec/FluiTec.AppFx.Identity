using System;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary
{
    /// <summary>   A database test.</summary>
    public abstract partial class DbTest
    {
        /// <summary>   (Unit Test Method) can create role claim.</summary>
        [TestMethod]
        public void CanCreateRoleClaim()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity { Id = Guid.NewGuid(), Name = "TestRole" });
            var roleClaim = uow.RoleClaimRepository.Add(new RoleClaimEntity
            {
                RoleId = role.Id, 
                Type = "tRoleClaim", 
                Value = "vRoleClaim"
            });

            Assert.IsTrue(roleClaim.Id > -1);
        }

        /// <summary>   (Unit Test Method) can read role claim.</summary>
        [TestMethod]
        public void CanReadRoleClaim()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity { Id = Guid.NewGuid(), Name = "TestRole" });
            var roleClaim = uow.RoleClaimRepository.Add(new RoleClaimEntity
            {
                RoleId = role.Id,
                Type = "tRoleClaim",
                Value = "vRoleClaim"
            });
            var dbEntity = uow.RoleClaimRepository.Get(roleClaim.Id);

            Assert.AreEqual(roleClaim.RoleId, dbEntity.RoleId);
        }

        /// <summary>   (Unit Test Method) can update role claim.</summary>
        [TestMethod]
        public void CanUpdateRoleClaim()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity { Id = Guid.NewGuid(), Name = "TestRole" });
            var roleClaim = uow.RoleClaimRepository.Add(new RoleClaimEntity
            {
                RoleId = role.Id,
                Type = "tRoleClaim",
                Value = "vRoleClaim"
            });

            roleClaim.Value = "vRoleClaim2";
            uow.RoleClaimRepository.Update(roleClaim);

            var dbEntity = uow.RoleClaimRepository.Get(roleClaim.Id);
            Assert.AreEqual(roleClaim.Value, dbEntity.Value);
        }

        /// <summary>   (Unit Test Method) can delete role claim.</summary>
        [TestMethod]
        public void CanDeleteRoleClaim()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity { Id = Guid.NewGuid(), Name = "TestRole" });
            var roleClaim = uow.RoleClaimRepository.Add(new RoleClaimEntity
            {
                RoleId = role.Id,
                Type = "tRoleClaim",
                Value = "vRoleClaim"
            });

            uow.RoleClaimRepository.Delete(roleClaim);
            var dbEntity = uow.RoleClaimRepository.Get(roleClaim.Id);
            Assert.IsNull(dbEntity);
        }
    }
}