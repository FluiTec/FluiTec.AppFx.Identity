using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A user migration. </summary>
    [DapperMigration(2020, 06, 24, 12, 37, "Achim Schnell")]
    public class _202006241237_UserMigration : FluentMigrator.Migration
    {
        /// <summary>Name of the unique mail index.</summary>
        private const string UniqueMailIndexName = "UX_NormalizedEmail";

        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ApplicationId").AsInt32().NotNullable()
                .WithColumn("Identifier").AsGuid().NotNullable().Unique()
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("NormalizedName").AsString(256).NotNullable().Indexed()
                .WithColumn("FullName").AsString(256).Nullable()
                .WithColumn("MobileAlias").AsString(16).Nullable()
                .WithColumn("IsAnonymous").AsBoolean().NotNullable()
                .WithColumn("LastActivityDate").AsDateTime().NotNullable()
                .WithColumn("PasswordHash").AsString(256).Nullable()
                .WithColumn("SecurityStamp").AsString(256).Nullable()
                .WithColumn("Email").AsString(256).NotNullable()
                .WithColumn("NormalizedEmail").AsString(256).NotNullable().Unique(UniqueMailIndexName)
                .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("Phone").AsString(256).Nullable()
                .WithColumn("PhoneConfirmed").AsBoolean().NotNullable()
                .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
                .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
                .WithColumn("AccessFailedCount").AsInt32().NotNullable()
                .WithColumn("LockedOutTill").AsDateTimeOffset().Nullable();

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserTable}")
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ApplicationId").AsInt32().NotNullable()
                .WithColumn("Identifier").AsCustom("CHAR(36)").NotNullable().Unique()
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("NormalizedName").AsString(256).NotNullable().Indexed()
                .WithColumn("FullName").AsString(256).Nullable()
                .WithColumn("MobileAlias").AsString(16).Nullable()
                .WithColumn("IsAnonymous").AsBoolean().NotNullable()
                .WithColumn("LastActivityDate").AsDateTime().NotNullable()
                .WithColumn("PasswordHash").AsString(256).Nullable()
                .WithColumn("SecurityStamp").AsString(256).Nullable()
                .WithColumn("Email").AsString(256).NotNullable()
                .WithColumn("NormalizedEmail").AsString(256).NotNullable().Unique(UniqueMailIndexName)
                .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("Phone").AsString(256).Nullable()
                .WithColumn("PhoneConfirmed").AsBoolean().NotNullable()
                .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
                .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
                .WithColumn("AccessFailedCount").AsInt32().NotNullable()
                .WithColumn("LockedOutTill").AsDateTime().Nullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserTable}");
        }
    }
}
