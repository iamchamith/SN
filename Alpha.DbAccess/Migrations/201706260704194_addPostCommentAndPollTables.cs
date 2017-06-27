namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPostCommentAndPollTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPostNeedComments",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false, maxLength: 1500),
                        ImageUrl = c.String(),
                        Tags = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.UserPostPolls",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(nullable: false, maxLength: 500),
                        Vs1Url = c.String(nullable: false, maxLength: 500),
                        Vs2Url = c.String(nullable: false, maxLength: 500),
                        Tags = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserPostPolls");
            DropTable("dbo.UserPostNeedComments");
        }
    }
}
