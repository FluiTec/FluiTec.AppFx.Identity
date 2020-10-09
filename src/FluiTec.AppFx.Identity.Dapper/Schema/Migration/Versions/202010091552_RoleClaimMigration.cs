using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A 202010091552 role claim migration.</summary>
    [DapperMigration(2020, 10, 09, 15, 52, "Achim Schnell")]
    public class _202010091552_RoleClaimMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the foreign key for role/roleclaim. </summary>
        private const string ForeignKeyRoleRoleClaim = "FK_Role_RoleClaim";

        /// <summary>   Collect the UP migration expressions.</summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.RoleClaimTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("RoleId").AsGuid().NotNullable()
                    .ForeignKey(ForeignKeyRoleRoleClaim, nameof(SchemaGlobals.RoleTable), nameof(RoleEntity.Id))
                .WithColumn("Type").AsString(256).NotNullable()
                .WithColumn("Value").AsString(256).Nullable();

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.RoleClaimTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("RoleId").AsCustom("CHAR(36)").NotNullable()
                .WithColumn("Type").AsString(256).NotNullable()
                .WithColumn("Value").AsString(256).Nullable();
        }

        /// <summary>   Collects the DOWN migration expressions.</summary>
        public override void Down()
        {
            // remove foreign keys
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyRoleRoleClaim)
                .OnTable(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema);

            // remove tables
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.RoleClaimTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.RoleClaimTable}");
        }
    }
}
