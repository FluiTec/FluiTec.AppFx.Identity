using FluentMigrator;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;
using FluiTec.AppFx.Identity.Data.Schema;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migrations
{
    /// <summary>
    ///     UserRole migration.
    /// </summary>
    [ExtendedMigration(2022,5,17,10,58, "Achim Schnell")]
    public class UserRoleMigration : Migration
    {
        private static readonly string ForeignKeyUserToUserRole =
            ForeignKeyIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.UserTable,
                SchemaGlobals.UserRoleTable);
        
        private static readonly string ForeignKeyRoleToUserRole=
            ForeignKeyIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.RoleTable,
                SchemaGlobals.UserRoleTable);
        
        
        /// <summary>
        ///     Applies the migration.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserRoleTable, true)
                .WithColumn(nameof(UserRoleEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserRoleEntity.UserId)).AsGuid().NotNullable()
                .WithColumn(nameof(UserRoleEntity.RoleId)).AsGuid().NotNullable();

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyUserToUserRole)
                .FromTable(SchemaGlobals.UserRoleTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(UserRoleEntity.UserId))
                .ToTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyRoleToUserRole)
                .FromTable(SchemaGlobals.UserRoleTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(UserRoleEntity.RoleId))
                .ToTable(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema);
            
            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserRoleTable, false)
                .WithColumn(nameof(UserRoleEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserRoleEntity.UserId)).AsCustom("CHAR(36)").NotNullable()
                .WithColumn(nameof(UserRoleEntity.RoleId)).AsCustom("CHAR(36)").NotNullable();
        }

        /// <summary>
        ///     Rolls back the migration.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyUserToUserRole);
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyRoleToUserRole);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserRoleTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserRoleTable, false);
        }
    }
}