using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities;

/// <summary>
/// A role claim entity.
/// </summary>
[EntityName(SchemaGlobals.Schema, SchemaGlobals.RoleClaimTable)]
public class RoleClaimEntity : BaseClaim, IKeyEntity<int>, IEquatable<RoleClaimEntity>
{
    /// <summary>	Gets or sets the identifier. </summary>
    /// <value>	The identifier. </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the role.
    /// </summary>
    ///
    /// <value>
    /// The identifier of the role.
    /// </value>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    ///
    /// <param name="other">    An object to compare with this object. </param>
    ///
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" />
    /// parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(RoleClaimEntity other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && RoleId.Equals(other.RoleId);
    }
}