using System.Text;
using FluentMigrator;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;
using FluiTec.AppFx.Data.Dapper.Extensions;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migrations
{
    /// <summary>
    ///     User migration.
    /// </summary>
    [ExtendedMigration(2022,5,17,10,25, "Achim Schnell")]
    public class UserMigration : Migration
    {
        /// <summary>
        ///     UniqueNormalizedEmail-Constraint.
        /// </summary>
        private static readonly string UniqueNormalizedEmailConstraint =
            UniqueIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.UserTable,
                nameof(UserEntity.NormalizedEmail));
        
        /// <summary>
        ///     Applies the migration.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserTable, true)
                .WithColumn(nameof(UserEntity.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn(nameof(UserEntity.Email)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.NormalizedEmail)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.EmailConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.Phone)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.PhoneConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.PasswordHash)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.SecurityStamp)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.TwoFactorEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.LockoutEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.AccessFailedCount)).AsInt32().NotNullable()
                .WithColumn(nameof(UserEntity.LockedOutTill)).AsDateTimeOffset().Nullable()
                .WithColumn(nameof(UserEntity.LockedOutPermanently)).AsBoolean().NotNullable();
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .UniqueConstraint(UniqueNormalizedEmailConstraint)
                .OnTable(SchemaGlobals.UserTable)
                .WithSchema(SchemaGlobals.Schema)
                .Column(nameof(UserEntity.NormalizedEmail));
            
            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserTable, false)
                .WithColumn(nameof(UserEntity.Id)).AsCustom("CHAR(36)").NotNullable().PrimaryKey()
                .WithColumn(nameof(UserEntity.Email)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.NormalizedEmail)).AsString(256).NotNullable()
                .WithColumn(nameof(UserEntity.EmailConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.Phone)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.PhoneConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.PasswordHash)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.SecurityStamp)).AsString(256).Nullable()
                .WithColumn(nameof(UserEntity.TwoFactorEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.LockoutEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(UserEntity.AccessFailedCount)).AsInt32().NotNullable()
                .WithColumn(nameof(UserEntity.LockedOutTill)).AsDateTime().Nullable()
                .WithColumn(nameof(UserEntity.LockedOutPermanently)).AsBoolean().NotNullable();
        }

        /// <summary>
        ///     Rolls back the migration.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .UniqueConstraint(UniqueNormalizedEmailConstraint);
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.UserTable, false);
        }
    }
}