namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moneyformepp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Shipment", "MoneyForMe");
            DropColumn("dbo.Shipment", "Expensive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipment", "Expensive", c => c.Double(nullable: false));
            AddColumn("dbo.Shipment", "MoneyForMe", c => c.Double(nullable: false));
        }
    }
}
