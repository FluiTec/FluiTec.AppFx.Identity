using System;
using System.Collections.Generic;
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

        /// <summary>   Gets or sets the name of the normalized. </summary>
        /// <value> The name of the normalized. </value>
        public string NormalizedName => Name?.ToUpper();

        /// <summary>	Gets or sets the phone. </summary>
        /// <value>	The phone. </value>
        public string Phone { get; set; }

        /// <summary>	Gets or sets a value indicating whether the phone confirmed. </summary>
        /// <value>	True if phone confirmed, false if not. </value>
        public bool PhoneConfirmed { get; set; }

        /// <summary>	Gets or sets the email. </summary>
        /// <value>	The email. </value>
        public string Email { get; set; }

        /// <summary>   Gets or sets the normalized email. </summary>
        /// <value> The normalized email. </value>
        public string NormalizedEmail => Email?.ToUpper();

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
    }

    /// <summary>   A user comparer.</summary>
    public class UserComparer : IEqualityComparer<UserEntity>
    {
        /// <summary>   Determines whether the specified objects are equal.</summary>
        /// <param name="x">    The first object of type T to compare. </param>
        /// <param name="y">    The second object of type T to compare. </param>
        /// <returns>   true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(UserEntity x, UserEntity y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id.Equals(y.Id) && x.Name == y.Name && x.Phone == y.Phone && x.PhoneConfirmed == y.PhoneConfirmed && x.Email == y.Email && x.EmailConfirmed == y.EmailConfirmed && x.FullName == y.FullName && x.PasswordHash == y.PasswordHash && x.SecurityStamp == y.SecurityStamp && x.TwoFactorEnabled == y.TwoFactorEnabled && x.LockoutEnabled == y.LockoutEnabled && x.AccessFailedCount == y.AccessFailedCount && Nullable.Equals(x.LockedOutTill, y.LockedOutTill);
        }

        /// <summary>   Returns a hash code for the specified object.</summary>
        /// <param name="obj">  The <see cref="T:System.Object"></see> for which a hash code is to be
        ///                     returned. </param>
        /// <returns>   A hash code for the specified object.</returns>
        public int GetHashCode(UserEntity obj)
        {
            unchecked
            {
                var hashCode = obj.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (obj.Name != null ? obj.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Phone != null ? obj.Phone.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.PhoneConfirmed.GetHashCode();
                hashCode = (hashCode * 397) ^ (obj.Email != null ? obj.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.EmailConfirmed.GetHashCode();
                hashCode = (hashCode * 397) ^ (obj.FullName != null ? obj.FullName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.PasswordHash != null ? obj.PasswordHash.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.SecurityStamp != null ? obj.SecurityStamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.TwoFactorEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.LockoutEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.AccessFailedCount;
                hashCode = (hashCode * 397) ^ obj.LockedOutTill.GetHashCode();
                return hashCode;
            }
        }
    }
}
