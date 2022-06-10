using FluentMigrator;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;
using FluiTec.AppFx.Identity.Data.Schema;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Identity.Data.Entities;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migrations
{
    /// <summary>
    ///     UserClaim migration.
    /// </summary>
    [ExtendedMigration(2022, 5, 17, 11, 17, "Achim Schnell")]
    public class UserClaimMigration : Migration
    {
        private static readonly string ForeignKeyUserToUserClaim =
            ForeignKeyIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.UserTable,
                SchemaGlobals.UserClaimTable);

        /// <summary>
        ///     Applies the migration.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserClaimTable, true)
                .WithColumn(nameof(UserClaimEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserClaimEntity.UserId)).AsGuid().NotNullable()
                .WithColumn(nameof(UserClaimEntity.Type)).AsString(256).NotNullable()
                .WithColumn(nameof(UserClaimEntity.Value)).AsString(256).Nullable();

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyUserToUserClaim)
                .FromTable(SchemaGlobals.UserClaimTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(UserClaimEntity.UserId))
                .ToTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserClaimTable, false)
                .WithColumn(nameof(UserClaimEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserClaimEntity.UserId)).AsGuid().NotNullable()
                .WithColumn(nameof(UserClaimEntity.Type)).AsCustom("CHAR(36)").NotNullable()
                .WithColumn(nameof(UserClaimEntity.Value)).AsCustom("CHAR(36)").Nullable();
        }

        /// <summary>
        ///     Rolls back the migration.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyUserToUserClaim);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserClaimTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserClaimTable, false);
        }
    }
}