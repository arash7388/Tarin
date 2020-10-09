namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manualwl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkLines", "Manual", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkLines", "Manual");
        }
    }
}
