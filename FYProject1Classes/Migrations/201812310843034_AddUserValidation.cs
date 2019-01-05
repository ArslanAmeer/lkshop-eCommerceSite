namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LoginID", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FullAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "FullAddress", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "LoginID", c => c.String());
            AlterColumn("dbo.Users", "FullName", c => c.String());
        }
    }
}
