using FluiTec.AppFx.Identity.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Identity.Tests.Data.Entities;

/// <summary>
/// (Unit Test Class) a role claim entity test.
/// </summary>
[TestClass]
public class RoleClaimEntityTest : EntityTest<RoleClaimEntity>
{
    /// <summary>
    /// Creates the entity.
    /// </summary>
    ///
    /// <returns>
    /// The new entity.
    /// </returns>
    protected override RoleClaimEntity CreateEntity()
    {
        return new RoleClaimEntity
        {
            Id = 1,
            RoleId = Guid.Parse("3AFD1C89-B8C7-4159-83C4-DE15C2BDE0AC"),
            Type = "superuser",
            Value = "true"
        };
    }

    /// <summary>
    /// Creates other entity.
    /// </summary>
    ///
    /// <returns>
    /// The new other entity.
    /// </returns>
    protected override RoleClaimEntity CreateOtherEntity()
    {
        return new RoleClaimEntity
        {
            Id = 2,
            RoleId = Guid.Parse("7ACC3181-6599-4302-B880-DC0E1C238922"),
            Type = "division",
            Value = "sales"
        };
    }
}