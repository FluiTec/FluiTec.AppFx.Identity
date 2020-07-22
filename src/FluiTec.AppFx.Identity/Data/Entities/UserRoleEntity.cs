using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities
{
    /// <summary>   A user role entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.UserRoleTable)]
    public class UserRoleEntity : IKeyEntity<int>
    {
        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }

        /// <summary>   Gets or sets the identifier of the user. </summary>
        /// <value> The identifier of the user. </value>
        public Guid UserId { get; set; }

        /// <summary>   Gets or sets the identifier of the role. </summary>
        /// <value> The identifier of the role. </value>
        public Guid RoleId { get; set; }
    }
}