using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A 202010091533 role migration.</summary>
    [DapperMigration(2020, 10, 09, 15, 33, "Achim Schnell")]
    public class _202010091533_RoleMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the unique name index. </summary>
        private const string UniqueNameIndexName = "UX_RoleName";

        /// <summary>   Name of the unique, normalized name index. </summary>
        private const string UniqueNormalizedNameIndexName = "UX_NormalizedRoleName";

        /// <summary>   Collect the UP migration expressions.</summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(RoleEntity.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn(nameof(RoleEntity.Name)).AsString(256).NotNullable().Unique(UniqueNameIndexName)
                .WithColumn(nameof(RoleEntity.NormalizedName)).AsString(256).NotNullable().Unique(UniqueNormalizedNameIndexName);

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.RoleTable}")
                .WithColumn(nameof(RoleEntity.Id)).AsCustom("CHAR(36)").NotNullable().PrimaryKey()
                .WithColumn(nameof(RoleEntity.Name)).AsString(256).NotNullable()
                .WithColumn(nameof(RoleEntity.NormalizedName)).AsString(256).NotNullable();
        }

        /// <summary>   Collects the DOWN migration expressions.</summary>
        public override void Down()
        {
            // remove tables
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.RoleTable}");
        }
    }
}
