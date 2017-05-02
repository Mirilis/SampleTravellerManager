namespace SampleTravellerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Work2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Travellers", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Travellers", "Status");
            DropColumn("dbo.Questions", "Status");
        }
    }
}
