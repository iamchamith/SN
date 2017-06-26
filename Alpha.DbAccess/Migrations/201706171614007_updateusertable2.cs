namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusertable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "IsValiedEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "MaritalStatus", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Token", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Token");
            DropColumn("dbo.User", "MaritalStatus");
            DropColumn("dbo.User", "IsValiedEmail");
        }
    }
}
