namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shipment", "UserId");
        }
    }
}
