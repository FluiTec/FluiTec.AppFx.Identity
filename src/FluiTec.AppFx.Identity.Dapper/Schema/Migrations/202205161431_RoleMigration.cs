using FluentMigrator;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;
using FluiTec.AppFx.Data.Dapper.Extensions;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migrations
{
    /// <summary>
    ///     Role migration.
    /// </summary>
    [ExtendedMigration(2022, 5, 16, 14, 31, "Achim Schnell")]
    public class RoleMigration : Migration
    {
        /// <summary>
        ///     UniqueName-Constraint.
        /// </summary>
        private static readonly string UniqueNormalizedNameConstraint =
            UniqueIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.RoleTable, nameof(RoleEntity.NormalizedName));

        /// <summary>
        ///     Applies the migration.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleTable, true)
                .WithColumn(nameof(RoleEntity.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn(nameof(RoleEntity.Name)).AsString(256).NotNullable()
                .WithColumn(nameof(RoleEntity.NormalizedName)).AsString(256).NotNullable();
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .UniqueConstraint(UniqueNormalizedNameConstraint)
                .OnTable(SchemaGlobals.RoleTable)
                .WithSchema(SchemaGlobals.Schema)
                .Column(nameof(RoleEntity.NormalizedName));

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleTable, false)
                .WithColumn(nameof(RoleEntity.Id)).AsCustom("CHAR(36)").NotNullable().PrimaryKey()
                .WithColumn(nameof(RoleEntity.Name)).AsString(256).NotNullable()
                .WithColumn(nameof(RoleEntity.NormalizedName)).AsString(256).NotNullable();
        }

        /// <summary>
        ///     Rolls back the migration.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .UniqueConstraint(UniqueNormalizedNameConstraint);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.RoleTable, false);
        }
    }
}