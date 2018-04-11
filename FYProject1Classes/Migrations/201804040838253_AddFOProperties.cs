namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFOProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinalOrders", "OrderNumber", c => c.String());
            AddColumn("dbo.FinalOrders", "OrderStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FinalOrders", "OrderStatus");
            DropColumn("dbo.FinalOrders", "OrderNumber");
        }
    }
}
