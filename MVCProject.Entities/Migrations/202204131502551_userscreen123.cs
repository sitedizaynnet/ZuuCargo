namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userscreen123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipment", "IsConfirmed", c => c.Boolean(nullable: false));
       
        }
        
        public override void Down()
        {
           
        }
    }
}
