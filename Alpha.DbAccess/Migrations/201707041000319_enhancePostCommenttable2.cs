namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostCommenttable2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PostComment");
            AddColumn("dbo.PostComment", "CommentId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PostComment", "CommentId");
            CreateIndex("dbo.PostComment", "PostId");
            AddForeignKey("dbo.PostComment", "PostId", "dbo.Post", "PostId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostComment", "PostId", "dbo.Post");
            DropIndex("dbo.PostComment", new[] { "PostId" });
            DropPrimaryKey("dbo.PostComment");
            DropColumn("dbo.PostComment", "CommentId");
            AddPrimaryKey("dbo.PostComment", "PostId");
        }
    }
}
