using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A claim migration. </summary>
    [DapperMigration(2020, 06, 25, 14, 03, "Achim Schnell")]
    public class _202006251403_ClaimMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the foreign key user. </summary>
        private const string ForeignKeyUserName = "FK_User_Claim";

        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.ClaimTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("Type").AsString(256).NotNullable()
                .WithColumn("Value").AsString(256).Nullable();
            IfDatabase("sqlserver", "postgres")
                .Create
                .ForeignKey(ForeignKeyUserName)
                .FromTable(SchemaGlobals.ClaimTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn("UserId")
                .ToTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn("Id");

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.ClaimTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("Type").AsString(256).NotNullable()
                .WithColumn("Value").AsString(256).Nullable();
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
                .Table(SchemaGlobals.ClaimTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.ClaimTable}");
        }
    }
}
