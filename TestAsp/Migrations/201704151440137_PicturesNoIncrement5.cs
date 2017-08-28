namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PicturesNoIncrement5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Pictures");
            AddColumn("dbo.Pictures", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Pictures", "ID");
            DropColumn("dbo.Pictures", "hjmhg");
            DropColumn("dbo.Pictures", "gfhj");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "gfhj", c => c.Int(nullable: false));
            AddColumn("dbo.Pictures", "hjmhg", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Pictures");
            DropColumn("dbo.Pictures", "ID");
            AddPrimaryKey("dbo.Pictures", "hjmhg");
        }
    }
}
