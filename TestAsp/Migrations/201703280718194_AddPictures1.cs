namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPictures1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pictures", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pictures", "Name", c => c.Int(nullable: false));
        }
    }
}
