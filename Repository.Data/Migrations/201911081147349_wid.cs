namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkLines", "WorksheetId", c => c.Int());
            CreateIndex("dbo.WorkLines", "WorksheetId");
            AddForeignKey("dbo.WorkLines", "WorksheetId", "dbo.Worksheets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkLines", "WorksheetId", "dbo.Worksheets");
            DropIndex("dbo.WorkLines", new[] { "WorksheetId" });
            DropColumn("dbo.WorkLines", "WorksheetId");
        }
    }
}
