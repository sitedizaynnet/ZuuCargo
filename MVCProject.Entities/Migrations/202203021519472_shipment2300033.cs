namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment2300033 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "Content", c => c.String());
            AddColumn("dbo.Shipment", "ValueOfPackage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "ValueOfPackage");
            DropColumn("dbo.Shipment", "Content");
        }
    }
}
