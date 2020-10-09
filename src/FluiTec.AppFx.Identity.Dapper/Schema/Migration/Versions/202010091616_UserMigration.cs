using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A 202010091616 user migration.</summary>
    [DapperMigration(2020, 10, 09, 16, 16, "Achim Schnell")]
    public class _202010091616_UserMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the unique, normalized name index. </summary>
        private const string UniqueNormalizedNameIndexName = "UX_NormalizedUserName";

        /// <summary>Name of the unique, normalized mail index.</summary>
        private const string UniqueNormalizedMailIndexName = "UX_NormalizedEmail";

        /// <summary>   Collect the UP migration expressions.</summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(UserEntity.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn(nameof(UserEntity.Name)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.NormalizedName)).AsString(256).NotNullable().Unique(UniqueNormalizedNameIndexName)
                .WithColumn(nameof(UserEntity.Phone)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.PhoneConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.Email)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.NormalizedEmail)).AsString(256).NotNullable().Unique(UniqueNormalizedMailIndexName)
                .WithColumn(nameof(UserEntity.EmailConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.FullName)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.PasswordHash)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.SecurityStamp)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.TwoFactorEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.LockoutEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.AccessFailedCount)).AsInt32().NotNullable()
                .WithColumn(nameof(UserEntity.LockedOutTill)).AsDateTimeOffset().Nullable()
                .WithColumn(nameof(UserEntity.LockedOutPermanently)).AsBoolean().NotNullable();

            IfDatabase("mysql")
                .Create
                .Table(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(UserEntity.Id)).AsCustom("CHAR(36)").NotNullable().PrimaryKey()
                .WithColumn(nameof(UserEntity.Name)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.NormalizedName)).AsString(256).NotNullable().Unique(UniqueNormalizedNameIndexName)
                .WithColumn(nameof(UserEntity.Phone)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.PhoneConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.Email)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.NormalizedEmail)).AsString(256).NotNullable().Unique(UniqueNormalizedMailIndexName)
                .WithColumn(nameof(UserEntity.EmailConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.FullName)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.PasswordHash)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.SecurityStamp)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.TwoFactorEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.LockoutEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.AccessFailedCount)).AsInt32().NotNullable()
                .WithColumn(nameof(UserEntity.LockedOutTill)).AsDateTime().Nullable()
                .WithColumn(nameof(UserEntity.LockedOutPermanently)).AsBoolean().NotNullable();
        }

        /// <summary>   Collects the DOWN migration expressions.</summary>
        public override void Down()
        {
            // remove constraints
            IfDatabase("sqlserver", "postgres")
                .Delete
                .UniqueConstraint(UniqueNormalizedNameIndexName)
                .FromTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("sqlserver", "postgres")
                .Delete
                .UniqueConstraint(UniqueNormalizedMailIndexName)
                .FromTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);

            // remove tables
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
