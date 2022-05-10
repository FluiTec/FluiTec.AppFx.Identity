﻿using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities;

/// <summary>
/// A user login entity.
/// </summary>
[EntityName(SchemaGlobals.Schema, SchemaGlobals.UserLoginTable)]
public class UserLoginEntity : IKeyEntity<int>
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    ///
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user.
    /// </summary>
    ///
    /// <value>
    /// The identifier of the user.
    /// </value>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the provider.
    /// </summary>
    ///
    /// <value>
    /// The provider.
    /// </value>
    public string Provider { get; set; }

    /// <summary>
    /// Gets or sets the provider key.
    /// </summary>
    ///
    /// <value>
    /// The provider key.
    /// </value>
    public string ProviderKey { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider dispay.
    /// </summary>
    ///
    /// <value>
    /// The name of the provider dispay.
    /// </value>
    public string ProviderDispayName { get; set; }
}