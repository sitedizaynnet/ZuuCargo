namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Price", "FromDesi", c => c.Double());
            AddColumn("dbo.Price", "ToDesi", c => c.Double());
            DropColumn("dbo.Price", "FromWeight");
            DropColumn("dbo.Price", "ToWeight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Price", "ToWeight", c => c.Double());
            AddColumn("dbo.Price", "FromWeight", c => c.Double());
            DropColumn("dbo.Price", "ToDesi");
            DropColumn("dbo.Price", "FromDesi");
        }
    }
}
