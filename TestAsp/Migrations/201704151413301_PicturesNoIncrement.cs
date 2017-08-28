namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PicturesNoIncrement : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Pictures");
            AlterColumn("dbo.Pictures", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Pictures", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Pictures");
            AlterColumn("dbo.Pictures", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Pictures", "ID");
        }
    }
}
