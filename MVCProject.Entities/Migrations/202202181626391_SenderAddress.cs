namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenderAddress : DbMigration
    {
        public override void Up()
        {
         
            AddColumn("dbo.Shipment", "SenderAddress", c => c.String());
            AddColumn("dbo.Shipment", "ReceiverAddress", c => c.String());
            AddColumn("dbo.Shipment", "TotalDolar", c => c.Double(nullable: false));
            AddColumn("dbo.Shipment", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "Notes");
            DropColumn("dbo.Shipment", "TotalDolar");
            DropColumn("dbo.Shipment", "ReceiverAddress");
            DropColumn("dbo.Shipment", "SenderAddress");
         
        }
    }
}
