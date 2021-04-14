namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberedit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ShareCount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "ShareCount");
        }
    }
}
