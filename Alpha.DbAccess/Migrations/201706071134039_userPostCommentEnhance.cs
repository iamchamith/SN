namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userPostCommentEnhance : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserPostComment");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPostComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserPostId = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 500),
                        UserId = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
