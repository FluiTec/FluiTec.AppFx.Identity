using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Identity.Data.Schema;

namespace FluiTec.AppFx.Identity.Dapper.Schema.Migration.Versions
{
    /// <summary>   A 202010091633 user claim migration.</summary>
    [DapperMigration(2020, 10, 09, 16, 33, "Achim Schnell")]
    public class _202010091633_UserClaimMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the foreign key for user/userclaim. </summary>
        private const string ForeignKeyUserUserClaim = "FK_User_UserClaim";

        /// <summary>   Collect the UP migration expressions.</summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.UserClaimTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(UserClaimEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserClaimEntity.UserId)).AsGuid().NotNullable()
                    .ForeignKey(ForeignKeyUserUserClaim, nameof(SchemaGlobals.UserTable), nameof(UserEntity.Id))
                .WithColumn(nameof(UserClaimEntity.Type)).AsString(256).NotNullable()
                .WithColumn(nameof(UserClaimEntity.Value)).AsString(256).Nullable();

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserClaimTable}")
                .WithColumn(nameof(UserClaimEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(UserClaimEntity.UserId)).AsCustom("CHAR(36)").NotNullable()
                .WithColumn(nameof(UserClaimEntity.Type)).AsString(256).NotNullable()
                .WithColumn(nameof(UserClaimEntity.Value)).AsString(256).Nullable();
        }

        /// <summary>   Collects the DOWN migration expressions.</summary>
        public override void Down()
        {
            // remove foreign keys
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyUserUserClaim)
                .OnTable(SchemaGlobals.UserTable)
                .InSchema(SchemaGlobals.Schema);

            // remove tables
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.UserClaimTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.UserClaimTable}");
        }
    }
}