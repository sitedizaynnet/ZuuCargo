namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment2300033233 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "HarmonyCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "HarmonyCode");
        }
    }
}
