namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "SenderTCNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "SenderTCNo");
        }
    }
}
