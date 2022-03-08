namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class receivere : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Shipment", "ChargeTo");
            DropColumn("dbo.Shipment", "ThirdPartyAccountNo");
            DropColumn("dbo.Shipment", "ThirdPartyCompanyName");
            DropColumn("dbo.Shipment", "ThirdPartyCountry");
            DropColumn("dbo.Shipment", "ServiceLevel");
            DropColumn("dbo.Shipment", "AdditionalHandlingCharge");
            DropColumn("dbo.Shipment", "LargePacketSurchargeApplies");
            DropColumn("dbo.Shipment", "ReferenceNo1");
            DropColumn("dbo.Shipment", "ReferenceNo2");
            DropColumn("dbo.Shipment", "DescriptionOfGoods");
            DropColumn("dbo.Shipment", "IsSample");
            DropColumn("dbo.Shipment", "IsDocument");
            DropColumn("dbo.Shipment", "IsSaturdayDelivery");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipment", "IsSaturdayDelivery", c => c.Boolean());
            AddColumn("dbo.Shipment", "IsDocument", c => c.Boolean());
            AddColumn("dbo.Shipment", "IsSample", c => c.Boolean());
            AddColumn("dbo.Shipment", "DescriptionOfGoods", c => c.String());
            AddColumn("dbo.Shipment", "ReferenceNo2", c => c.String());
            AddColumn("dbo.Shipment", "ReferenceNo1", c => c.String());
            AddColumn("dbo.Shipment", "LargePacketSurchargeApplies", c => c.Short());
            AddColumn("dbo.Shipment", "AdditionalHandlingCharge", c => c.Short());
            AddColumn("dbo.Shipment", "ServiceLevel", c => c.String());
            AddColumn("dbo.Shipment", "ThirdPartyCountry", c => c.String());
            AddColumn("dbo.Shipment", "ThirdPartyCompanyName", c => c.String());
            AddColumn("dbo.Shipment", "ThirdPartyAccountNo", c => c.Short());
            AddColumn("dbo.Shipment", "ChargeTo", c => c.String());
        }
    }
}
