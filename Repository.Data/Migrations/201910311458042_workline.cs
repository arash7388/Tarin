namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workline : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProcessId = c.Int(nullable: false),
                        OperatorId = c.Int(nullable: false),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OperatorId, cascadeDelete: true)
                .ForeignKey("dbo.Processes", t => t.ProcessId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProcessId)
                .Index(t => t.OperatorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkLines", "ProductId", "dbo.Products");
            DropForeignKey("dbo.WorkLines", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.WorkLines", "OperatorId", "dbo.Users");
            DropIndex("dbo.WorkLines", new[] { "OperatorId" });
            DropIndex("dbo.WorkLines", new[] { "ProcessId" });
            DropIndex("dbo.WorkLines", new[] { "ProductId" });
            DropTable("dbo.WorkLines");
        }
    }
}
