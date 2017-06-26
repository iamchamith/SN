namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserRelationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRelations",
                c => new
                    {
                        OwnerId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        IsFollowing = c.Boolean(nullable: false),
                        IsFollower = c.Boolean(nullable: false),
                        IsBlock = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerId, t.UserId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRelations");
        }
    }
}
