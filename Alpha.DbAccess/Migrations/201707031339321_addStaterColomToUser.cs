namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStaterColomToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "IsStarter", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "IsStarter");
        }
    }
}
