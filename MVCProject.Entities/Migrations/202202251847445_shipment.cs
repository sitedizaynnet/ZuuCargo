namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShipmentBarcodes",
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
            DropTable("dbo.ShipmentBarcodes");
        }
    }
}
