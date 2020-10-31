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

        }

        /// <summary>   (Unit Test Method) can update user.</summary>
        [TestMethod]
        public void CanUpdateUser()
        {

        }

        /// <summary>   (Unit Test Method) can delete user.</summary>
        [TestMethod]
        public void CanDeleteUser()
        {

        }
    }
}