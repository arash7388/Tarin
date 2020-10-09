namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class worksheet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorksheetDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorksheetId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Worksheets", t => t.WorksheetId, cascadeDelete: true)
                .Index(t => t.WorksheetId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Worksheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PartNo = c.String(),
                        ColorId = c.Int(nullable: false),
                        OperatorId = c.Int(),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.OperatorId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ColorId)
                .Index(t => t.OperatorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorksheetDetails", "WorksheetId", "dbo.Worksheets");
            DropForeignKey("dbo.Worksheets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Worksheets", "OperatorId", "dbo.Users");
            DropForeignKey("dbo.Worksheets", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.WorksheetDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.Worksheets", new[] { "OperatorId" });
            DropIndex("dbo.Worksheets", new[] { "ColorId" });
            DropIndex("dbo.Worksheets", new[] { "UserId" });
            DropIndex("dbo.WorksheetDetails", new[] { "ProductId" });
            DropIndex("dbo.WorksheetDetails", new[] { "WorksheetId" });
            DropTable("dbo.Worksheets");
            DropTable("dbo.WorksheetDetails");
        }
    }
}
