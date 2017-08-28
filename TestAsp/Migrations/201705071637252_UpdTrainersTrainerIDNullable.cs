namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTrainersTrainerIDNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "TrainerID", "dbo.Trainers");
            DropIndex("dbo.Groups", new[] { "TrainerID" });
            AlterColumn("dbo.Groups", "TrainerID", c => c.Int());
            CreateIndex("dbo.Groups", "TrainerID");
            AddForeignKey("dbo.Groups", "TrainerID", "dbo.Trainers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "TrainerID", "dbo.Trainers");
            DropIndex("dbo.Groups", new[] { "TrainerID" });
            AlterColumn("dbo.Groups", "TrainerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Groups", "TrainerID");
            AddForeignKey("dbo.Groups", "TrainerID", "dbo.Trainers", "ID", cascadeDelete: true);
        }
    }
}
