namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zonesp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Price", "IsExpress", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Price", "IsExpress");
        }
    }
}
