namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reklam0 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reklam", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reklam", "Title");
        }
    }
}
