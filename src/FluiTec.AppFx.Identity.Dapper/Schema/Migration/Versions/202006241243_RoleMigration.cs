using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A role migration. </summary>
    [DapperMigration(2020, 06, 24, 12, 43, "Achim Schnell")]
    public class _202006241243_RoleMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the unique name index. </summary>
        private const string UniqueNameIndexName = "UX_RoleName";

        /// <summary>   Name of the unique normalized name index. </summary>
        private const string UniqueNormalizedNameIndexName = "UX_NormalizedRoleName";

        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Identifier").AsGuid().NotNullable()
                .WithColumn("ApplicationId").AsInt32().NotNullable()
                .WithColumn("Name").AsString(256).NotNullable().Unique(UniqueNameIndexName)
                .WithColumn("NormalizedName").AsString(256).NotNullable().Unique(UniqueNormalizedNameIndexName)
                .WithColumn("Description").AsString(256).Nullable();

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.RoleTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Identifier").AsCustom("CHAR(36)").NotNullable()
                .WithColumn("ApplicationId").AsInt32().NotNullable()
                .WithColumn("Name").AsString(256).NotNullable().Unique(UniqueNameIndexName)
                .WithColumn("NormalizedName").AsString(256).NotNullable().Unique(UniqueNormalizedNameIndexName)
                .WithColumn("Description").AsString(256).Nullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
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