namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acodetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessCategories", "ProcessTime", c => c.Int(nullable: false));
            AddColumn("dbo.WorksheetDetails", "ACode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorksheetDetails", "ACode");
            DropColumn("dbo.ProcessCategories", "ProcessTime");
        }
    }
}
