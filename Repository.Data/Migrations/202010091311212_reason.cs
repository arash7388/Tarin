namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reason : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReworkReasons",
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
            DropTable("dbo.ReworkReasons");
        }
    }
}
