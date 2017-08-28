namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreModels1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialsContents",
                c => new
                    {
                        MaterialID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => new { t.MaterialID, t.ContentID })
                .ForeignKey("dbo.Materials", t => t.MaterialID, cascadeDelete: true)
                .Index(t => t.MaterialID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialsContents", "MaterialID", "dbo.Materials");
            DropIndex("dbo.MaterialsContents", new[] { "MaterialID" });
            DropTable("dbo.MaterialsContents");
        }
    }
}
