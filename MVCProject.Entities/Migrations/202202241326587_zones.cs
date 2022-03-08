namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zone", "IsExpress", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Zone", "IsExpress");
        }
    }
}
