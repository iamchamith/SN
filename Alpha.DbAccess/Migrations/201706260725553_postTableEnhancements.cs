namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postTableEnhancements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPostQuestions",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false, maxLength: 1500),
                    })
                .PrimaryKey(t => t.PostId);
            
            AddColumn("dbo.Post", "PostType", c => c.Int(nullable: false));
            DropColumn("dbo.Post", "Titile");
            DropColumn("dbo.Post", "Description");
            DropColumn("dbo.UserPostPolls", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPostPolls", "Tags", c => c.String());
            AddColumn("dbo.Post", "Description", c => c.String());
            AddColumn("dbo.Post", "Titile", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.Post", "PostType");
            DropTable("dbo.UserPostQuestions");
        }
    }
}
