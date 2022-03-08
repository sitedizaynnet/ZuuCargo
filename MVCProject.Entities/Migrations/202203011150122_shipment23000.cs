namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment23000 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "Konsimento", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "Konsimento");
        }
    }
}
