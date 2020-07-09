using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A user login migration. </summary>
    [DapperMigration(2020, 06, 24, 12, 53, "Achim Schnell")]
    public class _202006241253_UserLoginMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the unique name and key index. </summary>
        private const string UniqueNameAndKeyIndexName = "UX_ProviderName_ProviderKey";

        /// <summary>   The foreign key user login. </summary>
        private const string ForeignKeyUserUserLogin = "FK_User_UserLogin";

        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ProviderName").AsString(255).NotNullable()
                .WithColumn("ProviderKey").AsString(45).NotNullable()
                .WithColumn("ProviderDisplayName").AsString(255).Nullable()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("TimeStamp").AsDateTimeOffset().NotNullable();
            IfDatabase("sqlserver", "postgres")
                .Create
                .Index(UniqueNameAndKeyIndexName)
                .OnTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema)
                .OnColumn("ProviderName").Ascending()
                .OnColumn("ProviderKey").Ascending()
                .WithOptions().Unique();
            IfDatabase("sqlserver", "postgres")
                .Create
                .ForeignKey(ForeignKeyUserUserLogin)
                .FromTable(SchemaGlobals.UserLoginTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn("UserId")
                .ToTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn("Identifier");

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserLoginTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ProviderName").AsString(255).NotNullable()
                .WithColumn("ProviderKey").AsString(45).NotNullable()
                .WithColumn("ProviderDisplayName").AsString(255).Nullable()
                .WithColumn("UserId").AsCustom("CHAR(36)").NotNullable()
                .WithColumn("TimeStamp").AsDateTime().NotNullable();
            IfDatabase("mysql")
                .Create
                .Index(UniqueNameAndKeyIndexName)
                .OnTable($"{SchemaGlobals.Schema}_{SchemaGlobals.UserLoginTable}")
                .OnColumn("ProviderName").Ascending()
                .OnColumn("ProviderKey").Ascending()
                .WithOptions().Unique();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyUserUserLogin)
                .OnTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);

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
