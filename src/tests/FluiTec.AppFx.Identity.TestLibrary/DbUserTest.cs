using System;
using System.Linq;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary
{
    /// <summary>   A database test.</summary>
    public abstract partial class DbTest
    {
        /// <summary>   (Unit Test Method) can create user.</summary>
        [TestMethod]
        public void CanCreateUser()
        {
            AssertDbAvailable();
            
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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
            Assert.IsTrue(entity.Id != Guid.Empty);
        }

        /// <summary>   (Unit Test Method) can read user.</summary>
        [TestMethod]
        public void CanReadUser()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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

            var dbEntity = uow.UserRepository.Get(entity.Id);
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can read user by identifier.</summary>
        [TestMethod]
        public void CanReadUserByIdentifier()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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

            var dbEntity = uow.UserRepository.Get(entity.Id.ToString());
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can find user by normalized name.</summary>
        [TestMethod]
        public void CanFindUserByNormalizedName()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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

            var dbEntity = uow.UserRepository.FindByNormalizedName(entity.Name.ToUpper());
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can find user by normalized email.</summary>
        [TestMethod]
        public void CanFindUserByNormalizedEmail()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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

            var dbEntity = uow.UserRepository.FindByNormalizedName(entity.Email.ToUpper());
            Assert.AreEqual(entity.Email, dbEntity.Email);
        }

        /// <summary>   (Unit Test Method) can find user by identifiers.</summary>
        [TestMethod]
        public void CanFindUserByIds()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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

            var searchResult1 = uow.UserRepository.FindByIds(new[] { entity.Id });
            var searchResult2 = uow.UserRepository.FindByIds(new[] { entity.Id, Guid.NewGuid() });

            Assert.IsTrue(searchResult1.Any(sr => sr.Id == entity.Id));
            Assert.IsTrue(searchResult2.Any(sr => sr.Id == entity.Id));
        }

        /// <summary>   (Unit Test Method) can find by login.</summary>
        [TestMethod]
        public void CanFindByLogin()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity { UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth" });

            var dbEntity = uow.UserRepository.FindByLogin(userLogin.ProviderName, userLogin.ProviderKey);
            Assert.AreEqual(user.Id, dbEntity.Id);
        }

        /// <summary>   (Unit Test Method) can find all claims.</summary>
        [TestMethod]
        public void CanFindAllClaims()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var role = uow.RoleRepository.Add(new RoleEntity { Id = Guid.NewGuid(), Name = "TestRole" });
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });
            var roleClaim = uow.RoleClaimRepository.Add(new RoleClaimEntity
            {
                RoleId = role.Id,
                Type = "tRoleClaim",
                Value = "vRoleClaim"
            });

            var claims = uow.UserRepository.FindAllClaims(user).ToList();
            Assert.AreEqual(2, claims.Count);
            Assert.IsTrue(claims.Any(c => c.Value == userClaim.Value));
            Assert.IsTrue(claims.Any(c => c.Value == roleClaim.Value));
        }

        /// <summary>   (Unit Test Method) can update user.</summary>
        [TestMethod]
        public void CanUpdateUser()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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

            entity.Name = "m.mustermann@musterfirma.de2";
            uow.UserRepository.Update(entity);

            var dbEntity = uow.UserRepository.Get(entity.Id);
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can delete user.</summary>
        [TestMethod]
        public void CanDeleteUser()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
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
            entity.Name = "Test2";

            uow.UserRepository.Delete(entity);
            var dbEntity = uow.UserRepository.Get(entity.Id);
            Assert.IsNull(dbEntity);
        }
    }
}