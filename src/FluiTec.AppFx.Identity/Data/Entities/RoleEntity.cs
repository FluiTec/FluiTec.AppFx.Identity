using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities;

/// <summary>
/// A role entity.
/// </summary>
[EntityName(SchemaGlobals.Schema, SchemaGlobals.RoleTable)]
public class RoleEntity : IKeyEntity<Guid>
{
    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; set; }

    /// <summary>   Gets the name of the normalized. </summary>
    /// <value> The name of the normalized. </value>
    public string NormalizedName { get; set; }

    /// <summary>   Gets or sets the identifier. </summary>
    /// <value> The identifier. </value>
    public Guid Id { get; set; }
}