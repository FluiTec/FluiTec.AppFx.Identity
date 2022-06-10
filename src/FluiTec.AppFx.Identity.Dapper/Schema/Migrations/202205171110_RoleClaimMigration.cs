using FluentMigrator;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;
using FluiTec.AppFx.Identity.Data.Schema;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migrations
{
    /// <summary>
    /// RoleClaim migration.
    /// </summary>
    [ExtendedMigration(2022,5,17,11,10, "Achim Schnell")]
    public class RoleClaimMigration : Migration
    {
        private static readonly string ForeignKeyRoleToRoleClaim =
            ForeignKeyIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.RoleTable,
                SchemaGlobals.RoleClaimTable);
        
        /// <summary>
        ///     Applies the migration.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleClaimTable, true)
                .WithColumn(nameof(RoleClaimEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(RoleClaimEntity.RoleId)).AsGuid().NotNullable()
                .WithColumn(nameof(RoleClaimEntity.Type)).AsString(256).NotNullable()
                .WithColumn(nameof(RoleClaimEntity.Value)).AsString(256).Nullable();
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyRoleToRoleClaim)
                .FromTable(SchemaGlobals.RoleClaimTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(RoleClaimEntity.RoleId))
                .ToTable(SchemaGlobals.RoleTable)
                .InSchema(SchemaGlobals.Schema);
            
            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleClaimTable, false)
                .WithColumn(nameof(RoleClaimEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(RoleClaimEntity.RoleId)).AsGuid().NotNullable()
                .WithColumn(nameof(RoleClaimEntity.Type)).AsCustom("CHAR(36)").NotNullable()
                .WithColumn(nameof(RoleClaimEntity.Value)).AsCustom("CHAR(36)").Nullable();
        }

        /// <summary>
        ///     Rolls back the migration.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyRoleToRoleClaim);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleClaimTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleClaimTable, false);
        }
    }
}