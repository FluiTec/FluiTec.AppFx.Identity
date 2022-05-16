namespace FluiTec.AppFx.Identity.Data.Schema;

/// <summary>
///     A schema globals.
/// </summary>
public static class SchemaGlobals
{
    /// <summary>
    ///     (Immutable) the schema.
    /// </summary>
    public const string Schema = "AppFxIdentity";

    /// <summary>
    /// (Immutable) the role table.
    /// </summary>
    public const string RoleTable = "Role";

    /// <summary>
    /// (Immutable) the role claim table.
    /// </summary>
    public const string RoleClaimTable = "RoleClaim";

    /// <summary>
    /// (Immutable) the user table.
    /// </summary>
    public const string UserTable = "User";

    /// <summary>
    /// (Immutable) the user claim table.
    /// </summary>
    public const string UserClaimTable = "UserClaim";

    /// <summary>
    /// (Immutable) the user role table.
    /// </summary>
    public const string UserRoleTable = "UserRole";

    /// <summary>
    /// (Immutable) the user login table.
    /// </summary>
    public const string UserLoginTable = "UserLogin";
}