
namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posttableenhancement4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Post", "IsAnonymas");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "IsAnonymas", c => c.Boolean(nullable: false));
        }
    }
}
