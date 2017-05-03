namespace SampleTravellerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Work3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HelpImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Questions", "HelpImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "HelpImage", c => c.String());
            DropForeignKey("dbo.HelpImages", "Id", "dbo.Questions");
            DropIndex("dbo.HelpImages", new[] { "Id" });
            DropTable("dbo.HelpImages");
        }
    }
}
