namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTagDescriptionColom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tag", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tag", "Description");
        }
    }
}
