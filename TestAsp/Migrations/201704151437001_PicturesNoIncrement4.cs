namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PicturesNoIncrement4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Pictures");
            AddColumn("dbo.Pictures", "hjmhg", c => c.Int(nullable: false));
            AddColumn("dbo.Pictures", "gfhj", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Pictures", "hjmhg");
            DropColumn("dbo.Pictures", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "ID", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Pictures");
            DropColumn("dbo.Pictures", "gfhj");
            DropColumn("dbo.Pictures", "hjmhg");
            AddPrimaryKey("dbo.Pictures", "ID");
        }
    }
}
