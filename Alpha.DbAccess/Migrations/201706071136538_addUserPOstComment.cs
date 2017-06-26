namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserPOstComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPostComment",
                c => new
                    {
                        UserPostId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 500),
                        UserId = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserPostId);
            
            AddColumn("dbo.User", "UserPostComment_UserPostId", c => c.Guid());
            CreateIndex("dbo.User", "UserPostComment_UserPostId");
            AddForeignKey("dbo.User", "UserPostComment_UserPostId", "dbo.UserPostComment", "UserPostId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserPostComment_UserPostId", "dbo.UserPostComment");
            DropIndex("dbo.User", new[] { "UserPostComment_UserPostId" });
            DropColumn("dbo.User", "UserPostComment_UserPostId");
            DropTable("dbo.UserPostComment");
        }
    }
}
