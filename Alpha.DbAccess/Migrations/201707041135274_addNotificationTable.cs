namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNotificationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromUser = c.Guid(nullable: false),
                        ToUser = c.Guid(nullable: false),
                        Datetime = c.DateTime(nullable: false),
                        Description = c.String(),
                        NotificationType = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notifications");
        }
    }
}
