using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Entities.Base;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities
{
    /// <summary>   A role-claim entity.</summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.RoleClaimTable)]
    public class RoleClaimEntity : ClaimEntity, IKeyEntity<int>
    {
        /// <summary>   Gets or sets the identifier of the user. </summary>
        /// <value> The identifier of the user. </value>
        public Guid RoleId { get; set; }

        /// <summary>	Gets or sets the identifier. </summary>
        /// <value>	The identifier. </value>
        public int Id { get; set; }
    }
}