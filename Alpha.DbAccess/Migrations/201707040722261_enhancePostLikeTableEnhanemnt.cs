namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostLikeTableEnhanemnt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPost", "Likes", c => c.Int(nullable: false));
            AddColumn("dbo.UserPost", "Dislikes", c => c.Int(nullable: false));
            DropColumn("dbo.UserPost", "Like");
            DropColumn("dbo.UserPost", "Dislike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPost", "Dislike", c => c.Int(nullable: false));
            AddColumn("dbo.UserPost", "Like", c => c.Int(nullable: false));
            DropColumn("dbo.UserPost", "Dislikes");
            DropColumn("dbo.UserPost", "Likes");
        }
    }
}
