namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prcat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ProcessId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Processes", t => t.ProcessId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ProcessId);
            
            CreateTable(
                "dbo.Processes",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessCategories", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.ProcessCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProcessCategories", new[] { "ProcessId" });
            DropIndex("dbo.ProcessCategories", new[] { "CategoryId" });
            DropTable("dbo.Processes");
            DropTable("dbo.ProcessCategories");
        }
    }
}
