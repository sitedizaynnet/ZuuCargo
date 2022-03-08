namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class turk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Komerk",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(),
                        TotalWeight = c.Double(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Shipment", "ReceiverCity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "ReceiverCity");
            DropTable("dbo.Komerk");
        }
    }
}
