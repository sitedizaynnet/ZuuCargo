namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moneyforme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "MoneyForMe", c => c.Double(nullable: false));
            AddColumn("dbo.Shipment", "Expensive", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "Expensive");
            DropColumn("dbo.Shipment", "MoneyForMe");
        }
    }
}
