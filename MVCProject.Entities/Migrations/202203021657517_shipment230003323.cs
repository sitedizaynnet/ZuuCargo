namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment230003323 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShipmentTurpexBarcodes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        ShipmentId = c.Int(nullable: false),
                        Barcode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShipmentTurpexBarcodes");
        }
    }
}
