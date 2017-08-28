namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updParentsPhoneToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parents", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parents", "Phone", c => c.Int(nullable: false));
        }
    }
}
