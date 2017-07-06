namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolomPostTypeModeToPostLikeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostLikes", "PostLikeModeType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostLikes", "PostLikeModeType");
        }
    }
}
