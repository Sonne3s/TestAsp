namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreModels3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        First = c.String(),
                        Middle = c.String(),
                        Last = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        Weight = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        Phone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
