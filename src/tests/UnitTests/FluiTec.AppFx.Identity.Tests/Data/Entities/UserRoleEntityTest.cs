using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.Data.Entities;

/// <summary>
///     (Unit Test Class) a user role entity test.
/// </summary>
[TestClass]
public class UserRoleEntityTest : EntityTest<UserRoleEntity>
{
    /// <summary>
    ///     Creates the entity.
    /// </summary>
    /// <returns>
    ///     The new entity.
    /// </returns>
    protected override UserRoleEntity CreateEntity()
    {
        return new UserRoleEntity
        {
            Id = 1,
            RoleId = Guid.Parse("444B79F7-3FE4-41F0-A888-ABDB159AEF22"),
            UserId = Guid.Parse("5F9C0F76-8155-494C-B51D-A20E8C8E65FB")
        };
    }

    /// <summary>
    ///     Creates other entity.
    /// </summary>
    /// <returns>
    ///     The new other entity.
    /// </returns>
    protected override UserRoleEntity CreateOtherEntity()
    {
        return new UserRoleEntity
        {
            Id = 2,
            RoleId = Guid.Parse("F4C4AD06-A256-4C44-9E14-C8FC634192E1"),
            UserId = Guid.Parse("3E76710E-C926-4850-97C7-2DB2B19B1C6C")
        };
    }
}