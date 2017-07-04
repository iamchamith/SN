namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostLikeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostLikes", "PostId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostLikes", "PostId");
        }
    }
}
