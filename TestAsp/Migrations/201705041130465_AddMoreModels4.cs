namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreModels4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        First = c.String(),
                        Middle = c.String(),
                        Last = c.String(),
                        Phone = c.Int(nullable: false),
                        ChildID = c.Int(nullable: false),
                        Degree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.ChildID, cascadeDelete: true)
                .Index(t => t.ChildID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parents", "ChildID", "dbo.Students");
            DropIndex("dbo.Parents", new[] { "ChildID" });
            DropTable("dbo.Parents");
        }
    }
}
