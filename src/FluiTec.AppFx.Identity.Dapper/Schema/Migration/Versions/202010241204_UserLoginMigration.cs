using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A 202010241204 user login migration.</summary>
    [DapperMigration(2020,10,24,12,04, "Achim Schnell")]
    public class _202010241204_UserLoginMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the foreign key for user/userlogin. </summary>
        private const string ForeignKeyUserUserLogin = "FK_User_UserLogin";

        /// <summary>   Name of the index for uniq provider (ProviderName + ProviderKey). </summary>
        private const string IndexUniqueProvider = "IDX_UNIQUE_ProviderName_ProviderKey";

        /// <summary>   Collect the UP migration expressions.</summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(UserLoginEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserLoginEntity.ProviderName)).AsString(255).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderKey)).AsString(45).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderDisplayName)).AsString(255).Nullable()
                .WithColumn(nameof(UserLoginEntity.UserId)).AsGuid().NotNullable()
                .ForeignKey(ForeignKeyUserUserLogin, SchemaGlobals.UserTable, nameof(UserEntity.Id));

            IfDatabase("sqlserver", "postgres")
                .Create
                .Index(IndexUniqueProvider)
                .OnTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema)
                .OnColumn(nameof(UserLoginEntity.ProviderName)).Ascending()
                .OnColumn(nameof(UserLoginEntity.ProviderKey)).Ascending()
                .WithOptions().Unique();

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserLoginTable}")
                .WithColumn(nameof(UserLoginEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserLoginEntity.ProviderName)).AsString(255).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderKey)).AsString(45).NotNullable()
                .WithColumn(nameof(UserLoginEntity.ProviderDisplayName)).AsString(255).Nullable()
                .WithColumn(nameof(UserLoginEntity.UserId)).AsGuid().NotNullable();

            IfDatabase("mysql")
                .Create
                .Index(IndexUniqueProvider)
                .OnTable($"{SchemaGlobals.Schema}_{SchemaGlobals.UserLoginTable}")
                .OnColumn(nameof(UserLoginEntity.ProviderName)).Ascending()
                .OnColumn(nameof(UserLoginEntity.ProviderKey)).Ascending()
                .WithOptions().Unique();
        }

        /// <summary>   Collects the DOWN migration expressions.</summary>
        public override void Down()
        {
            // remove foreign keys
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyUserUserLogin)
                .OnTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema);

            // remove indexes
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Index(IndexUniqueProvider)
                .OnTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Index(IndexUniqueProvider)
                .OnTable($"{SchemaGlobals.Schema}_{SchemaGlobals.UserLoginTable}");

            // remove tables
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserLoginTable}");
        }
    }
}