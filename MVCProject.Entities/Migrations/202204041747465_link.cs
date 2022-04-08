namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class link : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "OtherLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "OtherLink");
        }
    }
}
