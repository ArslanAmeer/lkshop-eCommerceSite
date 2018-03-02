namespace FYProject1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Phone", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone", c => c.Int(nullable: false));
        }
    }
}
