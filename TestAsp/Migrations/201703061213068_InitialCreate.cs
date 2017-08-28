namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NewsContents",
                c => new
                    {
                        NewID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => new { t.NewID, t.ContentID })
                .ForeignKey("dbo.News", t => t.NewID, cascadeDelete: true)
                .Index(t => t.NewID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsContents", "NewID", "dbo.News");
            DropIndex("dbo.NewsContents", new[] { "NewID" });
            DropTable("dbo.NewsContents");
            DropTable("dbo.News");
        }
    }
}
