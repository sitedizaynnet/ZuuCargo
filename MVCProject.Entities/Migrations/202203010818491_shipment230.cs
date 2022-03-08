namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment230 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipment", "SenderPostalCode", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipment", "SenderPostalCode", c => c.Short());
        }
    }
}
