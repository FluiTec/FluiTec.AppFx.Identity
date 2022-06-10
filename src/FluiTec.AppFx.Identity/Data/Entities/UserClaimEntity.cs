using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities;

/// <summary>
/// A user claim entity.
/// </summary>
[EntityName(SchemaGlobals.Schema, SchemaGlobals.UserClaimTable)]
public class UserClaimEntity : BaseClaim, IKeyEntity<int>, IEquatable<UserClaimEntity>
{
    /// <summary>	Gets or sets the identifier. </summary>
    /// <value>	The identifier. </value>
    public int Id { get; set; }

    /// <summary>   Gets or sets the identifier of the user. </summary>
    /// <value> The identifier of the user. </value>
    public Guid UserId { get; set; }

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
    public bool Equals(UserClaimEntity other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && UserId.Equals(other.UserId);
    }
}