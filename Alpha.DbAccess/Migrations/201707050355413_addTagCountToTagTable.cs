namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTagCountToTagTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tag", "TagCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tag", "TagCount");
        }
    }
}
