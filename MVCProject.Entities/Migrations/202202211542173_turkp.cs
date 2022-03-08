namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class turkp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Price", "NameOfService");
            DropColumn("dbo.Price", "IsExpress");
            DropColumn("dbo.Zone", "IsExpress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zone", "IsExpress", c => c.Boolean());
            AddColumn("dbo.Price", "IsExpress", c => c.Boolean());
            AddColumn("dbo.Price", "NameOfService", c => c.String());
        }
    }
}
