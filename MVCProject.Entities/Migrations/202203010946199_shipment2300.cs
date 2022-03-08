namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment2300 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipment", "SenderIdVATNumber", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipment", "SenderIdVATNumber", c => c.Short());
        }
    }
}
