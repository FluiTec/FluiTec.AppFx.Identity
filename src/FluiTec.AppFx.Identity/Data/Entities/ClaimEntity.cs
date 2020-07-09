using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities
{
    /// <summary>   A claim entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.ClaimTable)]
    public class ClaimEntity : IKeyEntity<int>
    {
        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }

        /// <summary>	Gets or sets the identifier of the user. </summary>
        /// <value>	The identifier of the user. </value>
        public int UserId { get; set; }

        /// <summary>	Gets or sets the type. </summary>
        /// <value>	The type. </value>
        public string Type { get; set; }

        /// <summary>	Gets or sets the value. </summary>
        /// <value>	The value. </value>
        public string Value { get; set; }
    }
}