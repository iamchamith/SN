namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "Tags", c => c.String(nullable: false));
            AddColumn("dbo.UserPost", "Like", c => c.Int(nullable: false));
            AddColumn("dbo.UserPost", "Dislike", c => c.Int(nullable: false));
            DropColumn("dbo.Post", "Like");
            DropColumn("dbo.Post", "Deslike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "Deslike", c => c.Int(nullable: false));
            AddColumn("dbo.Post", "Like", c => c.Int(nullable: false));
            DropColumn("dbo.UserPost", "Dislike");
            DropColumn("dbo.UserPost", "Like");
            DropColumn("dbo.Post", "Tags");
        }
    }
}
