namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cameras", "AnnounceDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cameras", "AnnounceDate");
        }
    }
}
