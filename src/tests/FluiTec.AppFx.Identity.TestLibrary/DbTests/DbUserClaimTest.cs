using System;
using System.Linq;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.DbTests
{
    /// <summary>   A database test.</summary>
    public abstract partial class DbTest
    {
        /// <summary>   (Unit Test Method) can create User claim.</summary>
        [TestMethod]
        public void CanCreateUserClaim()
        {
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });

            Assert.IsTrue(userClaim.Id > -1);
        }

        /// <summary>   (Unit Test Method) can read User claim.</summary>
        [TestMethod]
        public void CanReadUserClaim()
        {
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });

            var dbEntity = uow.UserClaimRepository.Get(userClaim.Id);
            Assert.AreEqual(userClaim.UserId, dbEntity.UserId);
        }

        /// <summary>   (Unit Test Method) can get by user.</summary>
        [TestMethod]
        public void CanGetByUser()
        {
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });

            var userClaims = uow.UserClaimRepository.GetByUser(user);
            Assert.IsTrue(userClaims.Any(uc => uc.UserId == user.Id));
        }

        /// <summary>   Can get user identifiers for claim type.</summary>
        [TestMethod]
        public void CanGetUserIdsForClaimType()
        {
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });

            var uIds = uow.UserClaimRepository.GetUserIdsForClaimType(userClaim.Type);
            Assert.IsTrue(uIds.Any(uId => uId == user.Id));
        }

        /// <summary>   (Unit Test Method) can get by user and type.</summary>
        [TestMethod]
        public void CanGetByUserAndType()
        {
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });

            var dbEntity = uow.UserClaimRepository.GetByUserAndType(user, userClaim.Type);
            Assert.AreEqual(userClaim.Value, dbEntity.Value);
        }

        /// <summary>   (Unit Test Method) can update User claim.</summary>
        [TestMethod]
        public void CanUpdateUserClaim()
        {
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });

            userClaim.Value = "vUserClaim2";
            uow.UserClaimRepository.Update(userClaim);

            var dbEntity = uow.UserClaimRepository.Get(userClaim.Id);
            Assert.AreEqual(userClaim.Value, dbEntity.Value);
        }

        /// <summary>   (Unit Test Method) can delete User claim.</summary>
        [TestMethod]
        public void CanDeleteUserClaim()
        {
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
            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "tUserClaim",
                Value = "vUserClaim"
            });

            uow.UserClaimRepository.Delete(userClaim);
            var dbEntity = uow.UserClaimRepository.Get(userClaim.Id);
            Assert.IsNull(dbEntity);
        }
    }
}