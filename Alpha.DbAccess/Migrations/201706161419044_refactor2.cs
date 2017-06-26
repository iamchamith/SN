namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactor2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost");
            DropForeignKey("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost");
            DropIndex("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" });
            DropIndex("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" });
            CreateIndex("dbo.UserPost", "UserId");
            CreateIndex("dbo.UserPost", "PostId");
            AddForeignKey("dbo.UserPost", "PostId", "dbo.Post", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.UserPost", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            DropColumn("dbo.Post", "UserPost_UserId");
            DropColumn("dbo.Post", "UserPost_PostId");
            DropColumn("dbo.User", "UserPost_UserId");
            DropColumn("dbo.User", "UserPost_PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "UserPost_PostId", c => c.Guid());
            AddColumn("dbo.User", "UserPost_UserId", c => c.Guid());
            AddColumn("dbo.Post", "UserPost_PostId", c => c.Guid());
            AddColumn("dbo.Post", "UserPost_UserId", c => c.Guid());
            DropForeignKey("dbo.UserPost", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPost", "PostId", "dbo.Post");
            DropIndex("dbo.UserPost", new[] { "PostId" });
            DropIndex("dbo.UserPost", new[] { "UserId" });
            CreateIndex("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" });
            CreateIndex("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" });
            AddForeignKey("dbo.User", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost", new[] { "UserId", "PostId" });
            AddForeignKey("dbo.Post", new[] { "UserPost_UserId", "UserPost_PostId" }, "dbo.UserPost", new[] { "UserId", "PostId" });
        }
    }
}
