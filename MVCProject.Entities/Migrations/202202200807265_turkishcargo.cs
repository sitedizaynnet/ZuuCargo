namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class turkishcargo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TurkishCargo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AirBill = c.String(),
                        Price = c.Double(nullable: false),
                        TotalWeight = c.Double(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TurkishCargo");
        }
    }
}
