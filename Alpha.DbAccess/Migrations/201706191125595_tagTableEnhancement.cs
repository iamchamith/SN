namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagTableEnhancement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tag", "Owner", c => c.Guid(nullable: false));
            AddColumn("dbo.Tag", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tag", "CreatedDate");
            DropColumn("dbo.Tag", "Owner");
        }
    }
}
