namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deaia : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Zone", "Time");
            DropColumn("dbo.Zone", "IsExpress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zone", "IsExpress", c => c.Boolean(nullable: false));
            AddColumn("dbo.Zone", "Time", c => c.String());
        }
    }
}
