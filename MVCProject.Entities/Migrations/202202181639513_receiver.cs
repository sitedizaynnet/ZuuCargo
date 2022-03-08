namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class receiver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "ReceiverName", c => c.String());
            DropColumn("dbo.Shipment", "ContactPersonName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipment", "ContactPersonName", c => c.String());
            DropColumn("dbo.Shipment", "ReceiverName");
        }
    }
}
