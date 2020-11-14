using FluiTec.AppFx.Options.Attributes;

namespace FluiTec.AppFx.Identity.TestLibrary.Configuration
{
    /// <summary>   A pgsql admin options.</summary>
    [ConfigurationKey("Pgsql")]
    public class PgsqlAdminOptions : DbAdminOptions
    {
    }
}