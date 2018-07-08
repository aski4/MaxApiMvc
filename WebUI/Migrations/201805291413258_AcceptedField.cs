namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcceptedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Accepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Accepted");
        }
    }
}
