namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditBrandEntityClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Brand_Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "Brand_Image");
        }
    }
}
