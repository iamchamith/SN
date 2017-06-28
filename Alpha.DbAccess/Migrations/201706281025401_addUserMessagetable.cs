namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserMessagetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromUser = c.Guid(nullable: false),
                        ToUser = c.Guid(nullable: false),
                        Message = c.String(nullable: false, maxLength: 500),
                        SendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserMessages");
        }
    }
}
