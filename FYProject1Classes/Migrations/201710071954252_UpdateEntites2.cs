namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntites2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cameras", "Price", c => c.Single(nullable: false));
            AlterColumn("dbo.Cameras", "AnnounceDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cameras", "AnnounceDate", c => c.String());
            AlterColumn("dbo.Cameras", "Price", c => c.Long(nullable: false));
        }
    }
}
