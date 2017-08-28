namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopicOnPicturs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "Topic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pictures", "Topic");
        }
    }
}
