using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A 202010241230 user role migration.</summary>
    [DapperMigration(2020, 10, 24, 12, 30, "Achim Schnell")]
    public class _202010241230_UserRoleMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the foreign key for user/userrole. </summary>
        private const string ForeignKeyUserUserRole = "FK_UserRole_User";

        /// <summary>   Name of the foreign key for user/userrole. </summary>
        private const string ForeignKeyRoleUserRole = "FK_UserRole_Role";

        /// <summary>   Collect the UP migration expressions.</summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.UserRoleTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(UserRoleEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserRoleEntity.UserId)).AsGuid().NotNullable()
                .ForeignKey(ForeignKeyUserUserRole, SchemaGlobals.Schema, SchemaGlobals.UserTable,
                    nameof(UserEntity.Id))
                .WithColumn(nameof(UserRoleEntity.RoleId)).AsGuid().NotNullable()
                .ForeignKey(ForeignKeyRoleUserRole, SchemaGlobals.Schema, SchemaGlobals.RoleTable,
                    nameof(RoleEntity.Id));

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserRoleTable}")
                .WithColumn(nameof(UserRoleEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserRoleEntity.UserId)).AsGuid().NotNullable()
                .WithColumn(nameof(UserRoleEntity.RoleId)).AsGuid().NotNullable();
        }

        /// <summary>   Collects the DOWN migration expressions.</summary>
        public override void Down()
        {
            // remove foreign keys
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyUserUserRole)
                .OnTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyRoleUserRole)
                .OnTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema);

            // remove tables
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