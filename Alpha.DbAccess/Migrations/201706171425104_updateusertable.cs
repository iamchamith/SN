namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Country", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Language", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Language");
            DropColumn("dbo.User", "Country");
        }
    }
}
