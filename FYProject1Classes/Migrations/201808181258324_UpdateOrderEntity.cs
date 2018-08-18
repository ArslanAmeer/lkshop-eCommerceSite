namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShoppingCartItems", "Sale", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingCartItems", "Sale", c => c.Single(nullable: false));
        }
    }
}
