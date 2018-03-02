namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSaleEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cameras", "Sale", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cameras", "Sale", c => c.Int(nullable: false));
        }
    }
}
