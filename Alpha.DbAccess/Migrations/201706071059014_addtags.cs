namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 100),
                        UserTag_TagId = c.Int(),
                        UserTag_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTag", t => new { t.UserTag_TagId, t.UserTag_UserId })
                .Index(t => new { t.UserTag_TagId, t.UserTag_UserId });
            
            CreateTable(
                "dbo.UserPostComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserPostId = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 500),
                        UserId = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTag",
                c => new
                    {
                        TagId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => new { t.TagId, t.UserId });
            
            AddColumn("dbo.User", "UserTag_TagId", c => c.Int());
            AddColumn("dbo.User", "UserTag_UserId", c => c.Guid());
            CreateIndex("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" });
            AddForeignKey("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" }, "dbo.UserTag", new[] { "TagId", "UserId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" }, "dbo.UserTag");
            DropForeignKey("dbo.Tag", new[] { "UserTag_TagId", "UserTag_UserId" }, "dbo.UserTag");
            DropIndex("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" });
            DropIndex("dbo.Tag", new[] { "UserTag_TagId", "UserTag_UserId" });
            DropColumn("dbo.User", "UserTag_UserId");
            DropColumn("dbo.User", "UserTag_TagId");
            DropTable("dbo.UserTag");
            DropTable("dbo.UserPostComment");
            DropTable("dbo.Tag");
        }
    }
}
