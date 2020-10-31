using System;
using System.Linq;
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

        /// <summary>   (Unit Test Method) can get by role.</summary>
        [TestMethod]
        public void CanGetByRole()
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

            var roleClaims = uow.RoleClaimRepository.GetByRole(role);
            Assert.IsTrue(roleClaims.Any(rc => rc.RoleId == role.Id));
        }

        /// <summary>   (Unit Test Method) can get role identifiers for claim type.</summary>
        [TestMethod]
        public void CanGetRoleIdsForClaimType()
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

            var roleIds = uow.RoleClaimRepository.GetRoleIdsForClaimType(roleClaim.Type);
            Assert.IsTrue(roleIds.Any(rId => rId == role.Id));
        }

        /// <summary>   (Unit Test Method) can get by role and type.</summary>
        [TestMethod]
        public void CanGetByRoleAndType()
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

            var dbEntity = uow.RoleClaimRepository.GetByRoleAndType(role, roleClaim.Type);
            Assert.AreEqual(roleClaim.Value, dbEntity.Value);
        }

        /// <summary>   (Unit Test Method) can get users for claim type.</summary>
        [TestMethod]
        public void CanGetUsersForClaimType()
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
            var user = uow.UserRepository.Add(new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "m.mustermann@musterfirma.de",
                Email = "m.mustermann@musterfirma.de",
                EmailConfirmed = false,
                FullName = "Max Mustermann",
                AccessFailedCount = 0,
                LockedOutPermanently = false,
                LockedOutTill = null,
                LockoutEnabled = false,
                PasswordHash = "<>",
                Phone = "<>",
                PhoneConfirmed = false,
                SecurityStamp = "<>",
                TwoFactorEnabled = false
            });
            var userRole = uow.UserRoleRepository.Add(new UserRoleEntity {RoleId = role.Id, UserId = user.Id});

            var users = uow.RoleClaimRepository.GetUsersForClaimType(roleClaim.Type);
            Assert.IsTrue(users.Any(u => u.Id == user.Id));
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