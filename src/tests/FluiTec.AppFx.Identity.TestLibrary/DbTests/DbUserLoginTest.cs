using System;
using System.Linq;
using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.TestLibrary.DbTests
{
    /// <summary>   A database test.</summary>
    public abstract partial class DbTest
    {
        /// <summary>   (Unit Test Method) can create user login.</summary>
        [TestMethod]
        public void CanCreateUserLogin()
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity
                {UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth"});

            Assert.IsTrue(userLogin.Id > -1);
        }

        /// <summary>   (Unit Test Method) can read user login.</summary>
        [TestMethod]
        public void CanReadUserLogin()
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity
                {UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth"});

            var dbEntity = uow.LoginRepository.Get(userLogin.Id);
            Assert.AreEqual(userLogin.UserId, dbEntity.UserId);
        }

        /// <summary>   (Unit Test Method) can remove by name and key.</summary>
        [TestMethod]
        public void CanRemoveByNameAndKey()
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity
                {UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth"});

            uow.LoginRepository.RemoveByNameAndKey("Auth", "Auth");
            Assert.IsNull(uow.LoginRepository.FindByNameAndKey("Auth", "Auth"));
        }

        /// <summary>   (Unit Test Method) can find by user identifier.</summary>
        [TestMethod]
        public void CanFindByUserId()
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity
                {UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth"});

            var dbEntity = uow.LoginRepository.FindByUserId(user.Id).Single();
            Assert.AreEqual(userLogin.Id, dbEntity.Id);
        }

        /// <summary>   (Unit Test Method) can find by name and key.</summary>
        [TestMethod]
        public void CanFindByNameAndKey()
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity
                {UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth"});

            var dbEntity = uow.LoginRepository.FindByNameAndKey("Auth", "Auth");
            Assert.AreEqual(userLogin.Id, dbEntity.Id);
        }

        /// <summary>   (Unit Test Method) can update user login.</summary>
        [TestMethod]
        public void CanUpdateUserLogin()
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity
                {UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth"});

            userLogin.ProviderName = "Auth2";
            uow.LoginRepository.Update(userLogin);

            var dbEntity = uow.LoginRepository.Get(userLogin.Id);
            Assert.AreEqual(userLogin.ProviderName, dbEntity.ProviderName);
        }

        /// <summary>   (Unit Test Method) can delete user login.</summary>
        [TestMethod]
        public void CanDeleteUserLogin()
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
            var userLogin = uow.LoginRepository.Add(new UserLoginEntity
                {UserId = user.Id, ProviderKey = "Auth", ProviderDisplayName = "Auth", ProviderName = "Auth"});

            uow.LoginRepository.Delete(userLogin);
            var dbEntity = uow.LoginRepository.Get(userLogin.Id);
            Assert.IsNull(dbEntity);
        }
    }
}