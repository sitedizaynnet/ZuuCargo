namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moneyforbuy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "MoneyForBuy", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "MoneyForBuy");
        }
    }
}
