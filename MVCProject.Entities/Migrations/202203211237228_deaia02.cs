namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deaia02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "IsDelivered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "IsDelivered");
        }
    }
}
