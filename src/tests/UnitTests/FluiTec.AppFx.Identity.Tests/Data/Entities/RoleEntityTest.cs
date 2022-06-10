using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.Data.Entities;

/// <summary>
/// A role entity test.
/// </summary>
[TestClass]
public class RoleEntityTest : EntityTest<RoleEntity>
{
    /// <summary>
    /// Creates the entity.
    /// </summary>
    ///
    /// <returns>
    /// The new entity.
    /// </returns>
    protected override RoleEntity CreateEntity()
    {
        return new RoleEntity {Id = Guid.Parse("68985365-1828-40B3-B221-EFD2740EDDA4"), Name = "Administrator"};
    }

    /// <summary>
    /// Creates other entity.
    /// </summary>
    ///
    /// <returns>
    /// The new other entity.
    /// </returns>
    protected override RoleEntity CreateOtherEntity()
    {
        return new RoleEntity { Id = Guid.Parse("D99E13F7-6B0C-4AF6-A32A-CEB6C09CB821"), Name = "User" };
    }
}