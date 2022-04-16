namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agentSenderName : DbMigration
    {
        public override void Up()
        {
         
            AddColumn("dbo.Shipment", "AgentSenderName", c => c.String());
            
        }
        
        public override void Down()
        {
          
            DropColumn("dbo.Shipment", "AgentSenderName");
           
        }
    }
}
