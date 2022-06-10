using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.Data.Entities;

/// <summary>
/// A user entity test.
/// </summary>
[TestClass]
public class UserEntityTest : EntityTest<UserEntity>
{
    /// <summary>
    /// Creates the entity.
    /// </summary>
    ///
    /// <returns>
    /// The new entity.
    /// </returns>
    protected override UserEntity CreateEntity()
    {
        return new UserEntity
        {
            Id = Guid.Parse("485CC859-B630-42CD-9969-AC76E95DA47A"),
            Email = "t.mustermann@musterfirma.de",
            EmailConfirmed = true,
            Phone = "+49(1111)111111",
            PhoneConfirmed = true,
            PasswordHash = "",
            SecurityStamp = "",
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
            LockedOutPermanently = false,
            LockedOutTill = null
        };
    }

    /// <summary>
    /// Creates other entity.
    /// </summary>
    ///
    /// <returns>
    /// The new other entity.
    /// </returns>
    protected override UserEntity CreateOtherEntity()
    {
        return new UserEntity
        {
            Id = Guid.Parse("CAB3085C-86DF-4AFC-9935-E14CBDF88FA8"),
            Email = "m.mustermann@musterfirma.de",
            EmailConfirmed = true,
            Phone = "+49(2222)222222",
            PhoneConfirmed = true,
            PasswordHash = "",
            SecurityStamp = "",
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
            LockedOutPermanently = false,
            LockedOutTill = null
        };
    }
}