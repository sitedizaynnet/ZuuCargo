namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RemainingCost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAccounting = c.Double(),
                        TurkishCargo = c.Double(),
                        Komerk = c.Double(),
                        Taxi = c.Double(),
                        RemainingCosts = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RemainingCost");
        }
    }
}
