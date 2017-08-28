namespace TestAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Postfix", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "Postfix");
        }
    }
}
