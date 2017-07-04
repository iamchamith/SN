namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPostLikeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        UserPostId = c.Int(nullable: false),
                        IsLike = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostLikes", "UserId", "dbo.User");
            DropIndex("dbo.PostLikes", new[] { "UserId" });
            DropTable("dbo.PostLikes");
        }
    }
}
