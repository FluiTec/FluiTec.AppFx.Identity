using System;
using System.Linq;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.DbTests
{
    /// <summary>   A database test.</summary>
    public abstract class UserRoleDbTest : DbTest
    {
        /// <summary>   (Unit Test Method) can create user role.</summary>
        [TestMethod]
        public void CanCreateUserRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            Assert.IsTrue(userRole.Id > -1);
        }

        /// <summary>   (Unit Test Method) can read user role.</summary>
        [TestMethod]
        public void CanReadUserRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            var dbEntity = uow.UserRoleRepository.Get(userRole.Id);
            Assert.AreEqual(userRole.UserId, dbEntity.UserId);
        }

        /// <summary>   (Unit Test Method) can find by user identifier and role identifier.</summary>
        [TestMethod]
        public void CanFindByUserIdAndRoleId()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            var dbEntity = uow.UserRoleRepository.FindByUserIdAndRoleId(user.Id, role.Id);
            Assert.AreEqual(userRole.Id, dbEntity.Id);
        }

        /// <summary>   (Unit Test Method) can find by user.</summary>
        [TestMethod]
        public void CanFindByUser()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            var roles = uow.UserRoleRepository.FindByUser(user);
            Assert.IsTrue(roles.Any(r => r.Id == role.Id));
        }

        /// <summary>   (Unit Test Method) can find by role.</summary>
        [TestMethod]
        public void CanFindByRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            var users = uow.UserRoleRepository.FindByRole(role);
            Assert.IsTrue(users.Any(u => u.Id == user.Id));
        }

        /// <summary>   (Unit Test Method) can remove by user.</summary>
        [TestMethod]
        public void CanRemoveByUser()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            uow.UserRoleRepository.RemoveByUser(user);
            var dbEntity = uow.UserRoleRepository.Get(userRole.Id);
            Assert.IsNull(dbEntity);
        }

        /// <summary>   (Unit Test Method) can remove by role.</summary>
        [TestMethod]
        public void CanRemoveByRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            uow.UserRoleRepository.RemoveByRole(role);
            var dbEntity = uow.UserRoleRepository.Get(userRole.Id);
            Assert.IsNull(dbEntity);
        }

        /// <summary>   (Unit Test Method) can delete user role.</summary>
        [TestMethod]
        public void CanDeleteUserRole()
        {
            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity {Id = Guid.NewGuid(), Name = "TestRole"});
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

            uow.UserRoleRepository.Delete(userRole);
            var dbEntity = uow.UserRoleRepository.Get(userRole.Id);
            Assert.IsNull(dbEntity);
        }
    }
}