namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment230003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "Acente", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "Acente");
        }
    }
}
