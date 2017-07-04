namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostLikeService : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.User", "Email", unique: true, name: "EmailIndex");
            DropColumn("dbo.PostLikes", "UserPostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostLikes", "UserPostId", c => c.Int(nullable: false));
            DropIndex("dbo.User", "EmailIndex");
        }
    }
}
