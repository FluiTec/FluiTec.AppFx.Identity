using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities
{
    /// <summary>   A user entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.UserTable)]
    public class UserEntity : IKeyEntity<int>, ITimeStampedKeyEntity
    {
        #region Properties

        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }

        /// <summary>	Gets or sets the name. </summary>
        /// <value>	The name. </value>
        public string Name { get; set; }

        /// <summary>	Gets or sets the name of the lowered user. </summary>
        /// <value>	The name of the lowered user. </value>
        public string NormalizedName { get; set; }

        /// <summary>   Gets or sets the name of the full. </summary>
        /// <value> The name of the full. </value>
        public string FullName { get; set; }

        /// <summary>	Gets or sets the mobile alias. </summary>
        /// <value>	The mobile alias. </value>
        public string MobileAlias { get; set; }

        /// <summary>	Gets or sets a value indicating whether this object is anonymous. </summary>
        /// <value>	True if this object is anonymous, false if not. </value>
        public bool IsAnonymous { get; set; }

        /// <summary>	Gets or sets the last activity date. </summary>
        /// <value>	The last activity date. </value>
        public DateTime LastActivityDate { get; set; }

        /// <summary>	Gets or sets the password hash. </summary>
        /// <value>	The password hash. </value>
        public string PasswordHash { get; set; }

        /// <summary>	Gets or sets the security stamp. </summary>
        /// <value>	The security stamp. </value>

        public string SecurityStamp { get; set; }

        /// <summary>	Gets or sets the email. </summary>
        /// <value>	The email. </value>
        public string Email { get; set; }

        /// <summary>	Gets or sets the normalized email. </summary>
        /// <value>	The normalized email. </value>
        public string NormalizedEmail { get; set; }

        /// <summary>	Gets or sets a value indicating whether the email confirmed. </summary>
        /// <value>	True if email confirmed, false if not. </value>
        public bool EmailConfirmed { get; set; }

        /// <summary>	Gets or sets the phone. </summary>
        /// <value>	The phone. </value>
        public string Phone { get; set; }

        /// <summary>	Gets or sets a value indicating whether the phone confirmed. </summary>
        /// <value>	True if phone confirmed, false if not. </value>
        public bool PhoneConfirmed { get; set; }

        /// <summary>	Gets or sets a value indicating whether the two factor is enabled. </summary>
        /// <value>	True if two factor enabled, false if not. </value>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>	Gets or sets a value indicating whether the lockout is enabled. </summary>
        /// <value>	True if lockout enabled, false if not. </value>
        public bool LockoutEnabled { get; set; }

        /// <summary>	Gets or sets the number of access failed. </summary>
        /// <value>	The number of access failed. </value>
        public int AccessFailedCount { get; set; }

        /// <summary>	Gets or sets the Date/Time of the locked out till. </summary>
        /// <value>	The locked out till. </value>
        public DateTimeOffset? LockedOutTill { get; set; }

        /// <summary>   Gets or sets the timestamp. </summary>
        /// <value> The timestamp. </value>
        public DateTimeOffset TimeStamp { get; set; }

        #endregion

        #region Constructors

        /// <summary>   Default constructor. </summary>
        public UserEntity()
        {
            SecurityStamp = Guid.NewGuid().ToString();
        }

        #endregion

        #region Methods

        /// <summary>   Returns a string that represents the current object. </summary>
        /// <returns>   A string that represents the current object. </returns>
        public override string ToString()
        {
            return $"UserName: {Name}";
        }

        #endregion
    }
}
