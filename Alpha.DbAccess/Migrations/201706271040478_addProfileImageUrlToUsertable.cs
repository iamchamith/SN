namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProfileImageUrlToUsertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ProfileImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ProfileImage");
        }
    }
}
