namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvertisementPropValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdvertisementId = c.Int(nullable: false),
                        CategoryPropId = c.Int(nullable: false),
                        Value = c.String(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.AdvertisementId, cascadeDelete: true)
                .ForeignKey("dbo.CategoryProps", t => t.CategoryPropId)
                .Index(t => t.AdvertisementId)
                .Index(t => t.CategoryPropId);
            
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegDate = c.DateTime(),
                        ExpDate = c.DateTime(),
                        Mobile = c.String(),
                        Title = c.String(),
                        Desc = c.String(),
                        AdvMobile = c.String(),
                        AdvTel = c.String(),
                        AdvEmail = c.String(),
                        AdvWebsite = c.String(),
                        AdvCity = c.String(),
                        AreaId = c.Int(nullable: false),
                        AdvAddress = c.String(),
                        CategoryId = c.Int(nullable: false),
                        GPSLocation = c.String(),
                        UserIP = c.String(),
                        HideEmail = c.Boolean(nullable: false),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AreaId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AdvertisementPics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdvertisementId = c.Int(nullable: false),
                        PicHigh = c.Binary(),
                        PicLow = c.Binary(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.AdvertisementId, cascadeDelete: true)
                .Index(t => t.AdvertisementId);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CityId = c.Int(nullable: false),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        ParentId = c.Int(),
                        Image = c.Binary(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.CategoryProps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Caption = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        HasDatasource = c.Boolean(nullable: false),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.CategoryPropValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryPropId = c.Int(nullable: false),
                        Value = c.String(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryProps", t => t.CategoryPropId, cascadeDelete: true)
                .Index(t => t.CategoryPropId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Tel = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Fax = c.String(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InputOutputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        InOutType = c.Int(nullable: false),
                        ReceiptId = c.Int(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.InputOutputDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        Count = c.Int(nullable: false),
                        InputOutputId = c.Int(nullable: false),
                        ProductionQuality = c.Int(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.InputOutputs", t => t.InputOutputId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId)
                .Index(t => t.InputOutputId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ProductCategoryId = c.Int(nullable: false),
                        Image = c.Binary(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FriendlyName = c.String(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Href = c.String(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InputOutputs", "UserId", "dbo.Users");
            DropForeignKey("dbo.InputOutputDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.Categories");
            DropForeignKey("dbo.InputOutputDetails", "InputOutputId", "dbo.InputOutputs");
            DropForeignKey("dbo.InputOutputDetails", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CategoryPropValues", "CategoryPropId", "dbo.CategoryProps");
            DropForeignKey("dbo.AdvertisementPropValues", "CategoryPropId", "dbo.CategoryProps");
            DropForeignKey("dbo.CategoryProps", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Advertisements", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Advertisements", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Areas", "CityId", "dbo.Cities");
            DropForeignKey("dbo.AdvertisementPropValues", "AdvertisementId", "dbo.Advertisements");
            DropForeignKey("dbo.AdvertisementPics", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.InputOutputDetails", new[] { "InputOutputId" });
            DropIndex("dbo.InputOutputDetails", new[] { "CustomerId" });
            DropIndex("dbo.InputOutputDetails", new[] { "ProductId" });
            DropIndex("dbo.InputOutputs", new[] { "UserId" });
            DropIndex("dbo.CategoryPropValues", new[] { "CategoryPropId" });
            DropIndex("dbo.CategoryProps", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.Areas", new[] { "CityId" });
            DropIndex("dbo.AdvertisementPics", new[] { "AdvertisementId" });
            DropIndex("dbo.Advertisements", new[] { "CategoryId" });
            DropIndex("dbo.Advertisements", new[] { "AreaId" });
            DropIndex("dbo.AdvertisementPropValues", new[] { "CategoryPropId" });
            DropIndex("dbo.AdvertisementPropValues", new[] { "AdvertisementId" });
            DropTable("dbo.Tags");
            DropTable("dbo.Links");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.InputOutputDetails");
            DropTable("dbo.InputOutputs");
            DropTable("dbo.Customers");
            DropTable("dbo.CategoryPropValues");
            DropTable("dbo.CategoryProps");
            DropTable("dbo.Categories");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
            DropTable("dbo.AdvertisementPics");
            DropTable("dbo.Advertisements");
            DropTable("dbo.AdvertisementPropValues");
        }
    }
}
