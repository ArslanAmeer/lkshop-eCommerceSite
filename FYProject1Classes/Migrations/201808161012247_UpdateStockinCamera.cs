namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStockinCamera : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cameras", "Stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cameras", "Stock", c => c.String());
        }
    }
}
