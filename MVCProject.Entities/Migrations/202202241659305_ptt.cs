namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ptt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "ReceiverAddress2", c => c.String());
            AddColumn("dbo.Shipment", "Desi", c => c.Double(nullable: false));
            AddColumn("dbo.Shipment", "Height", c => c.Int(nullable: false));
            AddColumn("dbo.Shipment", "Lenght", c => c.Int(nullable: false));
            AddColumn("dbo.Shipment", "Weight", c => c.Int(nullable: false));
            AddColumn("dbo.Shipment", "Width", c => c.Int(nullable: false));
            AddColumn("dbo.Shipment", "PaymentMethod", c => c.String());
            AddColumn("dbo.Shipment", "ReceiverCountryId", c => c.String());
            AddColumn("dbo.Shipment", "ReceiverMail", c => c.String());
            AddColumn("dbo.Shipment", "PostType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "PostType");
            DropColumn("dbo.Shipment", "ReceiverMail");
            DropColumn("dbo.Shipment", "ReceiverCountryId");
            DropColumn("dbo.Shipment", "PaymentMethod");
            DropColumn("dbo.Shipment", "Width");
            DropColumn("dbo.Shipment", "Weight");
            DropColumn("dbo.Shipment", "Lenght");
            DropColumn("dbo.Shipment", "Height");
            DropColumn("dbo.Shipment", "Desi");
            DropColumn("dbo.Shipment", "ReceiverAddress2");
        }
    }
}
