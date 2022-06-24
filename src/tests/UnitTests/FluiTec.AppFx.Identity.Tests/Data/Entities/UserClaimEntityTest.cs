using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.Data.Entities;

/// <summary>
///     (Unit Test Class) a user claim entity test.
/// </summary>
[TestClass]
public class UserClaimEntityTest : EntityTest<UserClaimEntity>
{
    /// <summary>
    ///     Creates the entity.
    /// </summary>
    /// <returns>
    ///     The new entity.
    /// </returns>
    protected override UserClaimEntity CreateEntity()
    {
        return new UserClaimEntity
        {
            Id = 1,
            Type = "email",
            UserId = Guid.Parse("C124803A-491B-43B4-AE1F-BCD7BB17C91E"),
            Value = "t.mustermann@musterfirma.de"
        };
    }

    /// <summary>
    ///     Creates other entity.
    /// </summary>
    /// <returns>
    ///     The new other entity.
    /// </returns>
    protected override UserClaimEntity CreateOtherEntity()
    {
        return new UserClaimEntity
        {
            Id = 2,
            Type = "email",
            UserId = Guid.Parse("507EC673-0DA8-4E38-9742-CF1656870AD1"),
            Value = "m.mustermann@musterfirma.de"
        };
    }
}