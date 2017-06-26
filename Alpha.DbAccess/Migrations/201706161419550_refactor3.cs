namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactor3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "UserPostComment_UserPostId", "dbo.UserPostComment");
            DropIndex("dbo.User", new[] { "UserPostComment_UserPostId" });
            CreateIndex("dbo.UserPostComment", "UserId");
            AddForeignKey("dbo.UserPostComment", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            DropColumn("dbo.User", "UserPostComment_UserPostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "UserPostComment_UserPostId", c => c.Guid());
            DropForeignKey("dbo.UserPostComment", "UserId", "dbo.User");
            DropIndex("dbo.UserPostComment", new[] { "UserId" });
            CreateIndex("dbo.User", "UserPostComment_UserPostId");
            AddForeignKey("dbo.User", "UserPostComment_UserPostId", "dbo.UserPostComment", "UserPostId");
        }
    }
}
