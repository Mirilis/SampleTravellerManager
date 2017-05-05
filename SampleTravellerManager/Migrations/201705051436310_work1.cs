namespace SampleTravelerManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class work1 : DbMigration
    {
        #region Public Methods

        public override void Down()
        {
            DropForeignKey("dbo.ResponseRepository", "MilestoneId", "dbo.Travelers");
            DropForeignKey("dbo.Travelers", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.UserTeamAffiliation", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.UserTeamAffiliation", "TeamAffiliations_Id", "dbo.TeamAffiliations");
            DropForeignKey("dbo.Responses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Responses", "ResponseRepositoryId", "dbo.ResponseRepository");
            DropForeignKey("dbo.ResponseRepository", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.TravelerActionList", "Questions_Id", "dbo.Questions");
            DropForeignKey("dbo.TravelerActionList", "Milestones_Id", "dbo.Travelers");
            DropForeignKey("dbo.QuestionComments", "MilestoneId", "dbo.Travelers");
            DropForeignKey("dbo.QuestionComments", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Prerequisistes", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Prerequisistes", "PrerequisiteID", "dbo.Questions");
            DropForeignKey("dbo.Postrequisistes", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Postrequisistes", "PostrequisiteID", "dbo.Questions");
            DropForeignKey("dbo.HelpImages", "Id", "dbo.Questions");
            DropForeignKey("dbo.Corequisistes", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Corequisistes", "CorequisiteID", "dbo.Questions");
            DropIndex("dbo.UserTeamAffiliation", new[] { "Users_Id" });
            DropIndex("dbo.UserTeamAffiliation", new[] { "TeamAffiliations_Id" });
            DropIndex("dbo.TravelerActionList", new[] { "Questions_Id" });
            DropIndex("dbo.TravelerActionList", new[] { "Milestones_Id" });
            DropIndex("dbo.Prerequisistes", new[] { "QuestionID" });
            DropIndex("dbo.Prerequisistes", new[] { "PrerequisiteID" });
            DropIndex("dbo.Postrequisistes", new[] { "QuestionID" });
            DropIndex("dbo.Postrequisistes", new[] { "PostrequisiteID" });
            DropIndex("dbo.Corequisistes", new[] { "QuestionID" });
            DropIndex("dbo.Corequisistes", new[] { "CorequisiteID" });
            DropIndex("dbo.Responses", "IX_FK_ResponseRepositoryResponse");
            DropIndex("dbo.Responses", "IX_FK_ResponseUser");
            DropIndex("dbo.ResponseRepository", "IX_FK_QuestionResponseRepository");
            DropIndex("dbo.ResponseRepository", "IX_FK_MilestoneResponseRepository");
            DropIndex("dbo.Travelers", "IX_FK_Owner");
            DropIndex("dbo.QuestionComments", "IX_FK_QuestionResponseRepository");
            DropIndex("dbo.QuestionComments", "IX_FK_MilestoneResponseRepository");
            DropIndex("dbo.HelpImages", new[] { "Id" });
            DropTable("dbo.UserTeamAffiliation");
            DropTable("dbo.TravelerActionList");
            DropTable("dbo.Prerequisistes");
            DropTable("dbo.Postrequisistes");
            DropTable("dbo.Corequisistes");
            DropTable("dbo.TeamAffiliations");
            DropTable("dbo.Users");
            DropTable("dbo.Responses");
            DropTable("dbo.ResponseRepository");
            DropTable("dbo.Travelers");
            DropTable("dbo.QuestionComments");
            DropTable("dbo.HelpImages");
            DropTable("dbo.Questions");
        }

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
                    Template = c.Boolean(nullable: false),
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

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

            CreateTable(
                "dbo.QuestionComments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MilestoneId = c.Int(nullable: false),
                    QuestionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Travelers", t => t.MilestoneId, cascadeDelete: true)
                .Index(t => t.MilestoneId, name: "IX_FK_MilestoneResponseRepository")
                .Index(t => t.QuestionId, name: "IX_FK_QuestionResponseRepository");

            CreateTable(
                "dbo.Travelers",
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
                    Status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id, name: "IX_FK_Owner");

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
                .ForeignKey("dbo.Travelers", t => t.MilestoneId, cascadeDelete: true)
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
                    QueryResult = c.String(),
                    Comment = c.String(),
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
                "dbo.TravelerActionList",
                c => new
                {
                    Milestones_Id = c.Int(nullable: false),
                    Questions_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Milestones_Id, t.Questions_Id })
                .ForeignKey("dbo.Travelers", t => t.Milestones_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Questions_Id, cascadeDelete: true)
                .Index(t => t.Milestones_Id)
                .Index(t => t.Questions_Id);

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
        }

        #endregion Public Methods
    }
}