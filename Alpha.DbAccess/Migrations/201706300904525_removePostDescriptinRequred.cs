namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removePostDescriptinRequred : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostNeedComments", "Description", c => c.String(maxLength: 1500));
            AlterColumn("dbo.PostQuestions", "Description", c => c.String(maxLength: 1500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostQuestions", "Description", c => c.String(nullable: false, maxLength: 1500));
            AlterColumn("dbo.PostNeedComments", "Description", c => c.String(nullable: false, maxLength: 1500));
        }
    }
}
