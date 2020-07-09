using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities
{
    /// <summary>   A user login entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.UserLoginTable)]
    public class UserLoginEntity : IKeyEntity<int>, ITimeStampedKeyEntity
    {
        #region Properties

        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }

        /// <summary>   Gets or sets the name of the provider. </summary>
        /// <value> The name of the provider. </value>
        public string ProviderName { get; set; }

        /// <summary>   Gets or sets the provider key. </summary>
        /// <value> The provider key. </value>
        public string ProviderKey { get; set; }

        /// <summary>   Gets or sets the name of the provider display. </summary>
        /// <value> The name of the provider display. </value>
        public string ProviderDisplayName { get; set; }

        /// <summary>   Gets or sets the identifier of the user. </summary>
        /// <value> The identifier of the user. </value>
        public Guid UserId { get; set; }

        /// <summary>   Gets or sets the timestamp. </summary>
        /// <value> The timestamp. </value>
        public DateTimeOffset TimeStamp { get; set; }

        #endregion
    }
}