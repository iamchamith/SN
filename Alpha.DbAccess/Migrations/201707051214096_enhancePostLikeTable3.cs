namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostLikeTable3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostLikes", "Poll", c => c.Int(nullable: false));
            AddColumn("dbo.PostLikes", "PostType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostLikes", "PostType");
            DropColumn("dbo.PostLikes", "Poll");
        }
    }
}
