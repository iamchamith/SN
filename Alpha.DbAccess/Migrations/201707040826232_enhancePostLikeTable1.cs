namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostLikeTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostLikes", "PostLikeType", c => c.Int(nullable: false));
            DropColumn("dbo.PostLikes", "IsLike");
            DropColumn("dbo.PostLikes", "IsDisLike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostLikes", "IsDisLike", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostLikes", "IsLike", c => c.Boolean(nullable: false));
            DropColumn("dbo.PostLikes", "PostLikeType");
        }
    }
}
