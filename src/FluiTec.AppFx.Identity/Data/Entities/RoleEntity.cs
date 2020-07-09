using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities
{
    /// <summary>   A role entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.RoleTable)]
    public class RoleEntity : IKeyEntity<int>, ITimeStampedKeyEntity
    {
        #region Properties

        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }

        /// <summary>	Gets or sets the name. </summary>
        /// <value>	The name. </value>
        public string Name { get; set; }

        /// <summary>	Gets or sets the name of the lowered. </summary>
        /// <value>	The name of the lowered. </value>
        public string NormalizedName { get; set; }

        /// <summary>	Gets or sets the description. </summary>
        /// <value>	The description. </value>
        public string Description { get; set; }

        /// <summary>   Gets or sets the timestamp. </summary>
        /// <value> The timestamp. </value>
        public DateTimeOffset TimeStamp { get; set; }

        #endregion

        #region Constructors

        /// <summary>   Default constructor. </summary>
        public RoleEntity() {}

        /// <summary>   Constructor. </summary>
        /// <param name="name"> The name. </param>
        public RoleEntity(string name) : this()
        {
            Name = name;
        }

        #endregion

        #region Methods

        /// <summary>   Returns a string that represents the current object. </summary>
        /// <returns>   A string that represents the current object. </returns>
        public override string ToString()
        {
            return $"RoleName: {Name}";
        }

        #endregion
    }
}