namespace SampleTravellerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Work : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Team = c.Int(nullable: false),
                        RequiresResponse = c.Boolean(nullable: false),
                        Request = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        HelpText = c.String(nullable: false),
                        HelpImage = c.String(),
                        Template = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResponseRepository",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MilestoneId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Travellers", t => t.MilestoneId, cascadeDelete: true)
                .Index(t => t.MilestoneId, name: "IX_FK_MilestoneResponseRepository")
                .Index(t => t.QuestionId, name: "IX_FK_QuestionResponseRepository");
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        String = c.String(),
                        Integer = c.Int(),
                        Double = c.Double(),
                        File = c.Binary(),
                        Boolean = c.Boolean(),
                        EndDate = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                        Completed = c.Boolean(nullable: false),
                        Successful = c.Boolean(nullable: false),
                        ResponseRepositoryId = c.Int(nullable: false),
                        Query = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResponseRepository", t => t.ResponseRepositoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id, name: "IX_FK_ResponseUser")
                .Index(t => t.ResponseRepositoryId, name: "IX_FK_ResponseRepositoryResponse");
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        First = c.String(nullable: false),
                        Last = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        WindowsName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamAffiliations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Team = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Travellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Successful = c.Boolean(nullable: false),
                        Description = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id, name: "IX_FK_Owner");
            
            CreateTable(
                "dbo.Corequisistes",
                c => new
                    {
                        CorequisiteID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CorequisiteID, t.QuestionID })
                .ForeignKey("dbo.Questions", t => t.CorequisiteID)
                .ForeignKey("dbo.Questions", t => t.QuestionID)
                .Index(t => t.CorequisiteID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Postrequisistes",
                c => new
                    {
                        PostrequisiteID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostrequisiteID, t.QuestionID })
                .ForeignKey("dbo.Questions", t => t.PostrequisiteID)
                .ForeignKey("dbo.Questions", t => t.QuestionID)
                .Index(t => t.PostrequisiteID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Prerequisistes",
                c => new
                    {
                        PrerequisiteID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PrerequisiteID, t.QuestionID })
                .ForeignKey("dbo.Questions", t => t.PrerequisiteID)
                .ForeignKey("dbo.Questions", t => t.QuestionID)
                .Index(t => t.PrerequisiteID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.UserTeamAffiliation",
                c => new
                    {
                        TeamAffiliations_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamAffiliations_Id, t.Users_Id })
                .ForeignKey("dbo.TeamAffiliations", t => t.TeamAffiliations_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.TeamAffiliations_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.TravellerActionList",
                c => new
                    {
                        Milestones_Id = c.Int(nullable: false),
                        Questions_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Milestones_Id, t.Questions_Id })
                .ForeignKey("dbo.Travellers", t => t.Milestones_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Questions_Id, cascadeDelete: true)
                .Index(t => t.Milestones_Id)
                .Index(t => t.Questions_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travellers", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.ResponseRepository", "MilestoneId", "dbo.Travellers");
            DropForeignKey("dbo.TravellerActionList", "Questions_Id", "dbo.Questions");
            DropForeignKey("dbo.TravellerActionList", "Milestones_Id", "dbo.Travellers");
            DropForeignKey("dbo.UserTeamAffiliation", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.UserTeamAffiliation", "TeamAffiliations_Id", "dbo.TeamAffiliations");
            DropForeignKey("dbo.Responses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Responses", "ResponseRepositoryId", "dbo.ResponseRepository");
            DropForeignKey("dbo.ResponseRepository", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Prerequisistes", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Prerequisistes", "PrerequisiteID", "dbo.Questions");
            DropForeignKey("dbo.Postrequisistes", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Postrequisistes", "PostrequisiteID", "dbo.Questions");
            DropForeignKey("dbo.Corequisistes", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Corequisistes", "CorequisiteID", "dbo.Questions");
            DropIndex("dbo.TravellerActionList", new[] { "Questions_Id" });
            DropIndex("dbo.TravellerActionList", new[] { "Milestones_Id" });
            DropIndex("dbo.UserTeamAffiliation", new[] { "Users_Id" });
            DropIndex("dbo.UserTeamAffiliation", new[] { "TeamAffiliations_Id" });
            DropIndex("dbo.Prerequisistes", new[] { "QuestionID" });
            DropIndex("dbo.Prerequisistes", new[] { "PrerequisiteID" });
            DropIndex("dbo.Postrequisistes", new[] { "QuestionID" });
            DropIndex("dbo.Postrequisistes", new[] { "PostrequisiteID" });
            DropIndex("dbo.Corequisistes", new[] { "QuestionID" });
            DropIndex("dbo.Corequisistes", new[] { "CorequisiteID" });
            DropIndex("dbo.Travellers", "IX_FK_Owner");
            DropIndex("dbo.Responses", "IX_FK_ResponseRepositoryResponse");
            DropIndex("dbo.Responses", "IX_FK_ResponseUser");
            DropIndex("dbo.ResponseRepository", "IX_FK_QuestionResponseRepository");
            DropIndex("dbo.ResponseRepository", "IX_FK_MilestoneResponseRepository");
            DropTable("dbo.TravellerActionList");
            DropTable("dbo.UserTeamAffiliation");
            DropTable("dbo.Prerequisistes");
            DropTable("dbo.Postrequisistes");
            DropTable("dbo.Corequisistes");
            DropTable("dbo.Travellers");
            DropTable("dbo.TeamAffiliations");
            DropTable("dbo.Users");
            DropTable("dbo.Responses");
            DropTable("dbo.ResponseRepository");
            DropTable("dbo.Questions");
        }
    }
}
