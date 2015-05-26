namespace TouristClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializationDatabaseMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        DiaryId = c.Int(nullable: false),
                        SportsmanId = c.Int(nullable: false),
                        CampaignTimeinHour = c.Int(nullable: false),
                        CampaignTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CampaignTypes", t => t.CampaignTypeId)
                .ForeignKey("dbo.Sportsmen", t => t.SportsmanId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Diaries", t => t.DiaryId)
                .Index(t => t.CategoryId)
                .Index(t => t.DiaryId)
                .Index(t => t.SportsmanId)
                .Index(t => t.CampaignTypeId);
            
            CreateTable(
                "dbo.CampaignTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        TrainerId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainers", t => t.TrainerId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.TrainerId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Tourists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        PersonalDataId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalDatas", t => t.PersonalDataId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId)
                .Index(t => t.PersonalDataId);
            
            CreateTable(
                "dbo.PersonalDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Surname = c.String(maxLength: 30),
                        Patronymic = c.String(maxLength: 30),
                        Gender = c.String(maxLength: 10),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Heads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployDate = c.DateTime(nullable: false),
                        PersonalDataId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalDatas", t => t.PersonalDataId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.PersonalDataId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sportsmen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonalDataId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.PersonalDatas", t => t.PersonalDataId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.PersonalDataId)
                .Index(t => t.SectionId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Competitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Salary = c.Int(nullable: false),
                        SportsmanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sportsmen", t => t.SportsmanId, cascadeDelete: true)
                .Index(t => t.SportsmanId);
            
            CreateTable(
                "dbo.Tranings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place = c.String(maxLength: 30),
                        DateTime = c.DateTime(nullable: false),
                        TreningTimeInMinutes = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainers", t => t.TrainerId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.Diaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        StopTimeInMinutes = c.Int(nullable: false),
                        RoutePointId = c.Int(nullable: false),
                        DiaryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoutePoints", t => t.RoutePointId)
                .ForeignKey("dbo.Diaries", t => t.DiaryId, cascadeDelete: true)
                .Index(t => t.RoutePointId)
                .Index(t => t.DiaryId);
            
            CreateTable(
                "dbo.RoutePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompetitionSportsmen",
                c => new
                    {
                        Competition_Id = c.Int(nullable: false),
                        Sportsman_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Competition_Id, t.Sportsman_Id })
                .ForeignKey("dbo.Competitions", t => t.Competition_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sportsmen", t => t.Sportsman_Id, cascadeDelete: true)
                .Index(t => t.Competition_Id)
                .Index(t => t.Sportsman_Id);
            
            CreateTable(
                "dbo.CampaignTypeSections",
                c => new
                    {
                        CampaignType_Id = c.Int(nullable: false),
                        Section_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CampaignType_Id, t.Section_Id })
                .ForeignKey("dbo.CampaignTypes", t => t.CampaignType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.Section_Id, cascadeDelete: true)
                .Index(t => t.CampaignType_Id)
                .Index(t => t.Section_Id);
            
            CreateTable(
                "dbo.CampaignPersonalDatas",
                c => new
                    {
                        Campaign_Id = c.Int(nullable: false),
                        PersonalData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Campaign_Id, t.PersonalData_Id })
                .ForeignKey("dbo.Campaigns", t => t.Campaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.PersonalDatas", t => t.PersonalData_Id, cascadeDelete: true)
                .Index(t => t.Campaign_Id)
                .Index(t => t.PersonalData_Id);
            
            CreateTable(
                "dbo.CampaignRoutePoints",
                c => new
                    {
                        Campaign_Id = c.Int(nullable: false),
                        RoutePoint_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Campaign_Id, t.RoutePoint_Id })
                .ForeignKey("dbo.Campaigns", t => t.Campaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.RoutePoints", t => t.RoutePoint_Id, cascadeDelete: true)
                .Index(t => t.Campaign_Id)
                .Index(t => t.RoutePoint_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CampaignRoutePoints", "RoutePoint_Id", "dbo.RoutePoints");
            DropForeignKey("dbo.CampaignRoutePoints", "Campaign_Id", "dbo.Campaigns");
            DropForeignKey("dbo.CampaignPersonalDatas", "PersonalData_Id", "dbo.PersonalDatas");
            DropForeignKey("dbo.CampaignPersonalDatas", "Campaign_Id", "dbo.Campaigns");
            DropForeignKey("dbo.Stops", "DiaryId", "dbo.Diaries");
            DropForeignKey("dbo.Stops", "RoutePointId", "dbo.RoutePoints");
            DropForeignKey("dbo.Campaigns", "DiaryId", "dbo.Diaries");
            DropForeignKey("dbo.CampaignTypeSections", "Section_Id", "dbo.Sections");
            DropForeignKey("dbo.CampaignTypeSections", "CampaignType_Id", "dbo.CampaignTypes");
            DropForeignKey("dbo.Sportsmen", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Heads", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Groups", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Tourists", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Tourists", "PersonalDataId", "dbo.PersonalDatas");
            DropForeignKey("dbo.Sportsmen", "PersonalDataId", "dbo.PersonalDatas");
            DropForeignKey("dbo.Trainers", "SportsmanId", "dbo.Sportsmen");
            DropForeignKey("dbo.Tranings", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.Groups", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.CompetitionSportsmen", "Sportsman_Id", "dbo.Sportsmen");
            DropForeignKey("dbo.CompetitionSportsmen", "Competition_Id", "dbo.Competitions");
            DropForeignKey("dbo.Sportsmen", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Campaigns", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Campaigns", "SportsmanId", "dbo.Sportsmen");
            DropForeignKey("dbo.Heads", "PersonalDataId", "dbo.PersonalDatas");
            DropForeignKey("dbo.Campaigns", "CampaignTypeId", "dbo.CampaignTypes");
            DropIndex("dbo.CampaignRoutePoints", new[] { "RoutePoint_Id" });
            DropIndex("dbo.CampaignRoutePoints", new[] { "Campaign_Id" });
            DropIndex("dbo.CampaignPersonalDatas", new[] { "PersonalData_Id" });
            DropIndex("dbo.CampaignPersonalDatas", new[] { "Campaign_Id" });
            DropIndex("dbo.CampaignTypeSections", new[] { "Section_Id" });
            DropIndex("dbo.CampaignTypeSections", new[] { "CampaignType_Id" });
            DropIndex("dbo.CompetitionSportsmen", new[] { "Sportsman_Id" });
            DropIndex("dbo.CompetitionSportsmen", new[] { "Competition_Id" });
            DropIndex("dbo.Stops", new[] { "DiaryId" });
            DropIndex("dbo.Stops", new[] { "RoutePointId" });
            DropIndex("dbo.Tranings", new[] { "TrainerId" });
            DropIndex("dbo.Trainers", new[] { "SportsmanId" });
            DropIndex("dbo.Sportsmen", new[] { "CategoryId" });
            DropIndex("dbo.Sportsmen", new[] { "SectionId" });
            DropIndex("dbo.Sportsmen", new[] { "PersonalDataId" });
            DropIndex("dbo.Heads", new[] { "SectionId" });
            DropIndex("dbo.Heads", new[] { "PersonalDataId" });
            DropIndex("dbo.Tourists", new[] { "PersonalDataId" });
            DropIndex("dbo.Tourists", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "SectionId" });
            DropIndex("dbo.Groups", new[] { "TrainerId" });
            DropIndex("dbo.Campaigns", new[] { "CampaignTypeId" });
            DropIndex("dbo.Campaigns", new[] { "SportsmanId" });
            DropIndex("dbo.Campaigns", new[] { "DiaryId" });
            DropIndex("dbo.Campaigns", new[] { "CategoryId" });
            DropTable("dbo.CampaignRoutePoints");
            DropTable("dbo.CampaignPersonalDatas");
            DropTable("dbo.CampaignTypeSections");
            DropTable("dbo.CompetitionSportsmen");
            DropTable("dbo.RoutePoints");
            DropTable("dbo.Stops");
            DropTable("dbo.Diaries");
            DropTable("dbo.Tranings");
            DropTable("dbo.Trainers");
            DropTable("dbo.Competitions");
            DropTable("dbo.Categories");
            DropTable("dbo.Sportsmen");
            DropTable("dbo.Heads");
            DropTable("dbo.PersonalDatas");
            DropTable("dbo.Tourists");
            DropTable("dbo.Groups");
            DropTable("dbo.Sections");
            DropTable("dbo.CampaignTypes");
            DropTable("dbo.Campaigns");
        }
    }
}
