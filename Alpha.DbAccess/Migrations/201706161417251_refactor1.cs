namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactor1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tag", new[] { "UserTag_TagId", "UserTag_UserId" }, "dbo.UserTag");
            DropForeignKey("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" }, "dbo.UserTag");
            DropIndex("dbo.Tag", new[] { "UserTag_TagId", "UserTag_UserId" });
            DropIndex("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" });
            CreateIndex("dbo.UserTag", "TagId");
            CreateIndex("dbo.UserTag", "UserId");
            AddForeignKey("dbo.UserTag", "TagId", "dbo.Tag", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserTag", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            DropColumn("dbo.Tag", "UserTag_TagId");
            DropColumn("dbo.Tag", "UserTag_UserId");
            DropColumn("dbo.User", "UserTag_TagId");
            DropColumn("dbo.User", "UserTag_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "UserTag_UserId", c => c.Guid());
            AddColumn("dbo.User", "UserTag_TagId", c => c.Int());
            AddColumn("dbo.Tag", "UserTag_UserId", c => c.Guid());
            AddColumn("dbo.Tag", "UserTag_TagId", c => c.Int());
            DropForeignKey("dbo.UserTag", "UserId", "dbo.User");
            DropForeignKey("dbo.UserTag", "TagId", "dbo.Tag");
            DropIndex("dbo.UserTag", new[] { "UserId" });
            DropIndex("dbo.UserTag", new[] { "TagId" });
            CreateIndex("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" });
            CreateIndex("dbo.Tag", new[] { "UserTag_TagId", "UserTag_UserId" });
            AddForeignKey("dbo.User", new[] { "UserTag_TagId", "UserTag_UserId" }, "dbo.UserTag", new[] { "TagId", "UserId" });
            AddForeignKey("dbo.Tag", new[] { "UserTag_TagId", "UserTag_UserId" }, "dbo.UserTag", new[] { "TagId", "UserId" });
        }
    }
}
