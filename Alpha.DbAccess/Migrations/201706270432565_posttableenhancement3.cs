namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posttableenhancement3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserPostNeedComments", newName: "PostNeedComments");
            RenameTable(name: "dbo.UserPostPolls", newName: "PostPolls");
            RenameTable(name: "dbo.UserPostQuestions", newName: "PostQuestions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PostQuestions", newName: "UserPostQuestions");
            RenameTable(name: "dbo.PostPolls", newName: "UserPostPolls");
            RenameTable(name: "dbo.PostNeedComments", newName: "UserPostNeedComments");
        }
    }
}
