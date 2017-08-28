namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreModels2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateOf = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventsContents",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => new { t.EventID, t.ContentID })
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventsContents", "EventID", "dbo.Events");
            DropIndex("dbo.EventsContents", new[] { "EventID" });
            DropTable("dbo.EventsContents");
            DropTable("dbo.Events");
        }
    }
}
