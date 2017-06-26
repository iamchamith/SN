namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Titile = c.String(nullable: false, maxLength: 500),
                        Description = c.String(),
                        Like = c.Int(nullable: false),
                        Deslike = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Post");
        }
    }
}
