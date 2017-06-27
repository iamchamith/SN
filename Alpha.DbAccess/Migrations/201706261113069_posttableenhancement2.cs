namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posttableenhancement2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "Topic", c => c.String(nullable: false));
            AddColumn("dbo.Post", "IsAnonymas", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "IsAnonymas");
            DropColumn("dbo.Post", "Topic");
        }
    }
}
