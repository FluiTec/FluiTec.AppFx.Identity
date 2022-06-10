using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities;

/// <summary>
/// A role entity.
/// </summary>
[EntityName(SchemaGlobals.Schema, SchemaGlobals.RoleTable)]
public class RoleEntity : IKeyEntity<Guid>, IEquatable<RoleEntity>
{
    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; set; }

    /// <summary>   Gets or sets the normalized name. </summary>
    /// <value> The normalized name. </value>
    public string NormalizedName
    {
        get => Name?.ToUpper();
        // empty setter for nmemory
        // ReSharper disable once ValueParameterNotUsed
        set { }
    }

    /// <summary>   Gets or sets the identifier. </summary>
    /// <value> The identifier. </value>
    public Guid Id { get; set; }

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
    public bool Equals(RoleEntity other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Id.Equals(other.Id);
    }
}