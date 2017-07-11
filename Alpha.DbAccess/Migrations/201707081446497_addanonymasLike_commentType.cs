namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addanonymasLike_commentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostComment", "IsAnonymas", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostLikes", "IsAnonymas", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostLikes", "IsAnonymas");
            DropColumn("dbo.PostComment", "IsAnonymas");
        }
    }
}
