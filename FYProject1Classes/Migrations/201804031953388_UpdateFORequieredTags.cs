namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFORequieredTags : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FinalOrders", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.FinalOrders", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.FinalOrders", "FullAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FinalOrders", "FullAddress", c => c.String());
            AlterColumn("dbo.FinalOrders", "Email", c => c.String());
            AlterColumn("dbo.FinalOrders", "Name", c => c.String());
        }
    }
}
