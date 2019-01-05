namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserLoginValidation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "CityId_Id", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId_Id" });
            AlterColumn("dbo.Users", "CityId_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "CityId_Id");
            AddForeignKey("dbo.Users", "CityId_Id", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CityId_Id", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId_Id" });
            AlterColumn("dbo.Users", "CityId_Id", c => c.Int());
            CreateIndex("dbo.Users", "CityId_Id");
            AddForeignKey("dbo.Users", "CityId_Id", "dbo.Cities", "Id");
        }
    }
}
