using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Data.Entities;

/// <summary>
/// A user entity.
/// </summary>
[EntityName(SchemaGlobals.Schema, SchemaGlobals.UserTable)]
public class UserEntity : IKeyEntity<Guid>, IEquatable<UserEntity>
{
    #region Basic

    /// <summary>   Gets or sets the identifier. </summary>
    /// <value> The identifier. </value>
    public Guid Id { get; set; }

    /// <summary>	Gets or sets the email. </summary>
    /// <value>	The email. </value>
    public string Email { get; set; }

    /// <summary>   Gets or sets the normalized email. </summary>
    /// <value> The normalized email. </value>
    public string NormalizedEmail
    {
        get => Email?.ToUpper();
        // empty setter for nmemory
        // ReSharper disable once ValueParameterNotUsed
        set { }
    }

    /// <summary>	Gets or sets a value indicating whether the email confirmed. </summary>
    /// <value>	True if email confirmed, false if not. </value>
    public bool EmailConfirmed { get; set; }
    
    /// <summary>	Gets or sets the phone. </summary>
    /// <value>	The phone. </value>
    public string Phone { get; set; }

    /// <summary>	Gets or sets a value indicating whether the phone confirmed. </summary>
    /// <value>	True if phone confirmed, false if not. </value>
    public bool PhoneConfirmed { get; set; }

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

    /// <summary>   Gets or sets a value indicating whether the locked out permanently.</summary>
    /// <value> True if locked out permanently, false if not.</value>
    public bool LockedOutPermanently { get; set; }

    #endregion

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    ///
    /// <param name="other">    An object to compare with this object. </param>
    ///
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" />
    /// parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(UserEntity other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && Email == other.Email && EmailConfirmed == other.EmailConfirmed && Phone == other.Phone && PhoneConfirmed == other.PhoneConfirmed && PasswordHash == other.PasswordHash && SecurityStamp == other.SecurityStamp && TwoFactorEnabled == other.TwoFactorEnabled && LockoutEnabled == other.LockoutEnabled && AccessFailedCount == other.AccessFailedCount && Nullable.Equals(LockedOutTill, other.LockedOutTill) && LockedOutPermanently == other.LockedOutPermanently;
    }
}