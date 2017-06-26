namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userpost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPost",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        PostId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        IsPrimaryUser = c.Int(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        ParentPostId = c.Guid(nullable: false),
                        Anonymous = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.PostId });
            
            AddColumn("dbo.Post", "UserPost_UserId", c => c.Guid());
            AddColumn("dbo.Post", "UserPost_PostId", c => c.Guid());
            AddColumn("dbo.User", "UserPost_UserId", c => c.Guid());
            AddColumn("dbo.User", "UserPost_PostId", c => c.Guid());
            CreateIndex("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" });
            CreateIndex("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" });
            AddForeignKey("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost", new[] { "UserId", "PostId" });
            AddForeignKey("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost", new[] { "UserId", "PostId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost");
            DropForeignKey("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost");
            DropIndex("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" });
            DropIndex("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" });
            DropColumn("dbo.User", "UserPost_PostId");
            DropColumn("dbo.User", "UserPost_UserId");
            DropColumn("dbo.Post", "UserPost_PostId");
            DropColumn("dbo.Post", "UserPost_UserId");
            DropTable("dbo.UserPost");
        }
    }
}
