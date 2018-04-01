namespace FYProject1Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Brand_Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Level = c.String(),
                        Price = c.Single(nullable: false),
                        Sale = c.Single(nullable: false),
                        Kit = c.String(),
                        MegaPixel = c.String(),
                        SensorFormat = c.String(),
                        SensorType = c.String(),
                        FocusSystem = c.String(),
                        ISORange = c.String(),
                        VideoRecording = c.String(),
                        ShutterSpeed = c.String(),
                        VFType = c.String(),
                        ImageProcessor = c.String(),
                        LCDType = c.String(),
                        LCDDetail = c.String(),
                        BurstShot = c.String(),
                        LensMount = c.String(),
                        Wifi = c.Boolean(nullable: false),
                        Bluetooth = c.Boolean(nullable: false),
                        GPS = c.Boolean(nullable: false),
                        ExtMic = c.Boolean(nullable: false),
                        BuiltinFlash = c.Boolean(nullable: false),
                        WeatherSeal = c.Boolean(nullable: false),
                        cardslots = c.Boolean(nullable: false),
                        Description = c.String(),
                        AnnounceDate = c.DateTime(),
                        Brand_Id = c.Int(),
                        Category_Id = c.Int(),
                        Series_Id = c.Int(),
                        SubCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Series", t => t.Series_Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_Id)
                .Index(t => t.Brand_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Series_Id)
                .Index(t => t.SubCategory_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CameraImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Image_Url = c.String(nullable: false),
                        Camera_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cameras", t => t.Camera_Id)
                .Index(t => t.Camera_Id);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Brand_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .Index(t => t.Brand_Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FinalOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        FullAddress = c.String(),
                        Phone = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingCartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Sale = c.Single(nullable: false),
                        ImageURL = c.String(),
                        Quantity = c.Int(nullable: false),
                        FinalOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinalOrders", t => t.FinalOrder_Id)
                .Index(t => t.FinalOrder_Id);
            
            CreateTable(
                "dbo.MainBanners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Banner_Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        LoginID = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        FullAddress = c.String(),
                        BirthDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        Phone = c.Long(nullable: false),
                        SecurityQuestion = c.String(),
                        SecurityAnswer = c.String(),
                        Male = c.Boolean(nullable: false),
                        Female = c.Boolean(nullable: false),
                        Occupation = c.String(),
                        CityId_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.CityId_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Priority = c.Int(nullable: false),
                        Url = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserImages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "CityId_Id", "dbo.Cities");
            DropForeignKey("dbo.ShoppingCartItems", "FinalOrder_Id", "dbo.FinalOrders");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Cameras", "SubCategory_Id", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Cameras", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.Series", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.CameraImages", "Camera_Id", "dbo.Cameras");
            DropForeignKey("dbo.Cameras", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Cameras", "Brand_Id", "dbo.Brands");
            DropIndex("dbo.UserImages", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "CityId_Id" });
            DropIndex("dbo.ShoppingCartItems", new[] { "FinalOrder_Id" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropIndex("dbo.SubCategories", new[] { "Category_Id" });
            DropIndex("dbo.Series", new[] { "Brand_Id" });
            DropIndex("dbo.CameraImages", new[] { "Camera_Id" });
            DropIndex("dbo.Cameras", new[] { "SubCategory_Id" });
            DropIndex("dbo.Cameras", new[] { "Series_Id" });
            DropIndex("dbo.Cameras", new[] { "Category_Id" });
            DropIndex("dbo.Cameras", new[] { "Brand_Id" });
            DropTable("dbo.UserImages");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.MainBanners");
            DropTable("dbo.ShoppingCartItems");
            DropTable("dbo.FinalOrders");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Series");
            DropTable("dbo.CameraImages");
            DropTable("dbo.Categories");
            DropTable("dbo.Cameras");
            DropTable("dbo.Brands");
        }
    }
}
