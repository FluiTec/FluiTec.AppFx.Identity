using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A user role migration. </summary>
    [DapperMigration(2020, 06, 25, 13, 56, "Achim Schnell")]
    public class _202006251356_UserRoleMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the foreign key user. </summary>
        private const string ForeignKeyUserName = "FK_User_UserRole";

        /// <summary>   Name of the foreign key role. </summary>
        private const string ForeignKeyRoleName = "FK_Role_UserRole";

        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.UserRoleTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable();
            IfDatabase("sqlserver", "postgres")
                .Create
                .ForeignKey(ForeignKeyUserName)
                .FromTable(SchemaGlobals.UserRoleTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn("UserId")
                .ToTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn("Id");
            IfDatabase("sqlserver", "postgres")
                .Create
                .ForeignKey(ForeignKeyRoleName)
                .FromTable(SchemaGlobals.UserRoleTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn("RoleId")
                .ToTable(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn("Id");

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserRoleTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyUserName)
                .OnTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyRoleName)
                .OnTable(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema);
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.UserRoleTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserRoleTable}");
        }
    }
}
