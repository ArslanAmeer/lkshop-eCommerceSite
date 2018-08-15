namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockPropAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cameras", "Stock", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cameras", "Stock");
        }
    }
}
