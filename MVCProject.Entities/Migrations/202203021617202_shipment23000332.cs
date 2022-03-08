namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment23000332 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "ReceiverCityId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "ReceiverCityId");
        }
    }
}
