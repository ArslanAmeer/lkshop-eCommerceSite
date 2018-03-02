namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCameraProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cameras", "Category_Id", c => c.Int());
            CreateIndex("dbo.Cameras", "Category_Id");
            AddForeignKey("dbo.Cameras", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cameras", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Cameras", new[] { "Category_Id" });
            DropColumn("dbo.Cameras", "Category_Id");
        }
    }
}
