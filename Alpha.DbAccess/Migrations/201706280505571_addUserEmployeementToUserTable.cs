namespace Alpha.DbAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserEmployeementToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Employeement", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Employeement");
        }
    }
}
