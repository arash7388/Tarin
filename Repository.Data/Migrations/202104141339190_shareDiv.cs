namespace Repository.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shareDiv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShareDivisionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        ShareAmount = c.Long(nullable: false),
                        ShareDivisionId = c.Int(nullable: false),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.ShareDivisions", t => t.ShareDivisionId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.ShareDivisionId);
            
            CreateTable(
                "dbo.ShareDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Byte(nullable: false),
                        Amount = c.Long(nullable: false),
                        InsertDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeleteDateTime = c.DateTime(),
                        Status = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShareDivisionDetails", "ShareDivisionId", "dbo.ShareDivisions");
            DropForeignKey("dbo.ShareDivisionDetails", "MemberId", "dbo.Members");
            DropIndex("dbo.ShareDivisionDetails", new[] { "ShareDivisionId" });
            DropIndex("dbo.ShareDivisionDetails", new[] { "MemberId" });
            DropTable("dbo.ShareDivisions");
            DropTable("dbo.ShareDivisionDetails");
        }
    }
}
