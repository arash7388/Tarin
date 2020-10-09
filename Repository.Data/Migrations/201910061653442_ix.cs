namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProcessCategories", new[] { "CategoryId" });
            DropIndex("dbo.ProcessCategories", new[] { "ProcessId" });
            CreateIndex("dbo.ProcessCategories", new[] { "CategoryId", "ProcessId" }, unique: true, name: "IX_CatPr");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProcessCategories", "IX_CatPr");
            CreateIndex("dbo.ProcessCategories", "ProcessId");
            CreateIndex("dbo.ProcessCategories", "CategoryId");
        }
    }
}
