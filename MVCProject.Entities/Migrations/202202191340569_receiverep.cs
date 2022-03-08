namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class receiverep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "ExternalTrackingCode", c => c.String());
            AddColumn("dbo.Shipment", "IsPtt", c => c.Boolean(nullable: false));
            AddColumn("dbo.Shipment", "IsDhl", c => c.Boolean(nullable: false));
            DropColumn("dbo.Shipment", "DHLLink");
            DropColumn("dbo.Shipment", "TNTLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipment", "TNTLink", c => c.String());
            AddColumn("dbo.Shipment", "DHLLink", c => c.String());
            DropColumn("dbo.Shipment", "IsDhl");
            DropColumn("dbo.Shipment", "IsPtt");
            DropColumn("dbo.Shipment", "ExternalTrackingCode");
        }
    }
}
