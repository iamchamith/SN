namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enhancePostLikeTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostLikes", "IsDisLike", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostLikes", "IsDisLike");
        }
    }
}
