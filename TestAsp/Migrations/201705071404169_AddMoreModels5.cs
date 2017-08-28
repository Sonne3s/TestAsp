namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreModels5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sort = c.Int(nullable: false),
                        TrainerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trainers", t => t.TrainerID, cascadeDelete: true)
                .Index(t => t.TrainerID);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        First = c.String(),
                        Middle = c.String(),
                        Last = c.String(),
                        Phone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        STime = c.DateTime(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "TrainerID", "dbo.Trainers");
            DropIndex("dbo.Schedules", new[] { "GroupID" });
            DropIndex("dbo.Groups", new[] { "TrainerID" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Trainers");
            DropTable("dbo.Groups");
        }
    }
}
