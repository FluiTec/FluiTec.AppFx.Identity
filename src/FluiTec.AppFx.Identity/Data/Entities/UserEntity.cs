using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities
{
    /// <summary>   A user entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.UserTable)]
    public class UserEntity : IKeyEntity<Guid>
    {
        #region Basic

        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public Guid Id { get; set; }

        /// <summary>   Gets or sets the name. </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>	Gets or sets the phone. </summary>
        /// <value>	The phone. </value>
        public string Phone { get; set; }

        /// <summary>	Gets or sets a value indicating whether the phone confirmed. </summary>
        /// <value>	True if phone confirmed, false if not. </value>
        public bool PhoneConfirmed { get; set; }

        /// <summary>	Gets or sets the email. </summary>
        /// <value>	The email. </value>
        public string Email { get; set; }

        /// <summary>	Gets or sets a value indicating whether the email confirmed. </summary>
        /// <value>	True if email confirmed, false if not. </value>
        public bool EmailConfirmed { get; set; }

        /// <summary>   Gets or sets the name of the full. </summary>
        /// <value> The name of the full. </value>
        public string FullName { get; set; }

        #endregion

        #region Security

        /// <summary>	Gets or sets the password hash. </summary>
        /// <value>	The password hash. </value>
        public string PasswordHash { get; set; }

        /// <summary>	Gets or sets the security stamp. </summary>
        /// <value>	The security stamp. </value>
        public string SecurityStamp { get; set; }

        /// <summary>	Gets or sets a value indicating whether the two factor is enabled. </summary>
        /// <value>	True if two factor enabled, false if not. </value>
        public bool TwoFactorEnabled { get; set; }

        #endregion

        #region Lockout

        /// <summary>	Gets or sets a value indicating whether the lockout is enabled. </summary>
        /// <value>	True if lockout enabled, false if not. </value>
        public bool LockoutEnabled { get; set; }

        /// <summary>	Gets or sets the number of access failed. </summary>
        /// <value>	The number of access failed. </value>
        public int AccessFailedCount { get; set; }

        /// <summary>	Gets or sets the Date/Time of the locked out till. </summary>
        /// <value>	The locked out till. </value>
        public DateTimeOffset? LockedOutTill { get; set; }

        #endregion

        #region Methods

        /// <summary>   Normalize name. </summary>
        /// <param name="name"> The name. </param>
        /// <returns>   A string. </returns>
        public string NormalizeName(string name) => name?.ToUpper();

        /// <summary>   Normalize mail. </summary>
        /// <param name="mail"> The mail. </param>
        /// <returns>   A string. </returns>
        public string NormalizeMail(string mail) => mail?.ToUpper();

        #endregion
    }
}
