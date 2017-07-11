namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserExparePostTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPost", "ResponseMinits", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPost", "ResponseMinits");
        }
    }
}
