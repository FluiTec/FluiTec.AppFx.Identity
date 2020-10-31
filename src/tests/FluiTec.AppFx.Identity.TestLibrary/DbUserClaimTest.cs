using System;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary
{
    /// <summary>   A database test.</summary>
    public abstract partial class DbTest
    {
        /// <summary>   (Unit Test Method) can create User claim.</summary>
        [TestMethod]
        public void CanCreateUserClaim()
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
            throw new NotImplementedException();
        }

        /// <summary>   Can get user identifiers for claim type.</summary>
        [TestMethod]
        public void CanGetUserIdsForClaimType()
        {
            throw new NotImplementedException();
        }

        /// <summary>   (Unit Test Method) can get by user and type.</summary>
        [TestMethod]
        public void CanGetByUserAndType()
        {
            throw new NotImplementedException();
        }

        /// <summary>   (Unit Test Method) can update User claim.</summary>
        [TestMethod]
        public void CanUpdateUserClaim()
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