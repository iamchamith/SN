namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostCommenttable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPostComment", "UserId", "dbo.User");
            DropIndex("dbo.UserPostComment", new[] { "UserId" });
            CreateTable(
                "dbo.PostComment",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 500),
                        UserId = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.UserPostComment");
        }
        
        public override void Down()
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
            
            DropForeignKey("dbo.PostComment", "UserId", "dbo.User");
            DropIndex("dbo.PostComment", new[] { "UserId" });
            DropTable("dbo.PostComment");
            CreateIndex("dbo.UserPostComment", "UserId");
            AddForeignKey("dbo.UserPostComment", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
