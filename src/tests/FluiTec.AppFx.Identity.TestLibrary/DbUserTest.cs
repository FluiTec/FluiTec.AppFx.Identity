using System;
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
            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.UserRepository.Add(new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "m.mustermann@musterfirma.de", Email = "m.mustermann@musterfirma.de", EmailConfirmed = false,
                FullName = "Max Mustermann", AccessFailedCount = 0, LockedOutPermanently = false, LockedOutTill = null,
                LockoutEnabled = false, PasswordHash = "<>", Phone = "<>", PhoneConfirmed = false, SecurityStamp = "<>",
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