using FluentMigrator;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;
using FluiTec.AppFx.Identity.Data.Schema;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migrations
{
    /// <summary>
    ///     UserLogin migration.
    /// </summary>
    [ExtendedMigration(2022,5,17,11,19, "Achim Schnell")]
    public class UserLoginMigration : Migration
    {
        private static readonly string ForeignKeyUserToUserLogin =
            ForeignKeyIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.UserTable,
                SchemaGlobals.UserLoginTable);
        
        /// <summary>
        ///     Applies the migration.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserLoginTable, true)
                .WithColumn(nameof(UserLoginEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserLoginEntity.Provider)).AsString(255).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderKey)).AsString(45).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderDispayName)).AsString(255).Nullable()
                .WithColumn(nameof(UserLoginEntity.UserId)).AsGuid().NotNullable();
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyUserToUserLogin)
                .FromTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(UserLoginEntity.UserId))
                .ToTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserLoginTable, false)
                .WithColumn(nameof(UserLoginEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserLoginEntity.Provider)).AsString(255).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderKey)).AsString(45).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderDispayName)).AsString(255).Nullable()
                .WithColumn(nameof(UserLoginEntity.UserId)).AsCustom("CHAR(36)").NotNullable();
        }

        /// <summary>
        ///     Rolls back the migration.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyUserToUserLogin);
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserLoginTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserLoginTable, false);
        }
    }
}