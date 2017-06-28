namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserPreferencesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPreferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        UserPreference = c.Int(nullable: false),
                        State = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPreferences", "UserId", "dbo.User");
            DropIndex("dbo.UserPreferences", new[] { "UserId" });
            DropTable("dbo.UserPreferences");
        }
    }
}
