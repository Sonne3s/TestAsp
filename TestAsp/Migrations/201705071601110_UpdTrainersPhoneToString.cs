namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdTrainersPhoneToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainers", "Phone", c => c.Int(nullable: false));
        }
    }
}
