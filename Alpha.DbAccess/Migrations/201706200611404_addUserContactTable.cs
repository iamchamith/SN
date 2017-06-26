namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserContactTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        SocialNetwork = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 50),
                        Url = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserContacts", "UserId", "dbo.User");
            DropIndex("dbo.UserContacts", new[] { "UserId" });
            DropTable("dbo.UserContacts");
        }
    }
}
