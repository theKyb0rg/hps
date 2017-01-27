namespace HPFS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageAccessed = c.String(),
                        DateAccessed = c.DateTime(),
                        UserActionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserActions", t => t.UserActionID, cascadeDelete: true)
                .Index(t => t.UserActionID);
            
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DistanceGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalDistance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoalStartDate = c.DateTime(),
                        GoalEndDate = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Distances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistanceCount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DistanceDate = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CalendarEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CalendarEventName = c.String(maxLength: 250),
                        CalendarEventDescription = c.String(),
                        CalendarEventDate = c.DateTime(),
                        RoleName = c.String(maxLength: 50),
                        FileId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.HPSUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(),
                        FitBitUserId = c.String(maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MinuteGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalMinute = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoalStartDate = c.DateTime(),
                        GoalEndDate = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Minutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinuteCount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinuteDate = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                        Priority = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        NotificationDate = c.DateTime(),
                        CalendarEventId = c.Int(),
                        FileId = c.Int(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StepGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalSteps = c.Int(nullable: false),
                        GoalStartDate = c.DateTime(),
                        GoalEndDate = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StepCount = c.Int(nullable: false),
                        StepDate = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Company = c.String(maxLength: 50),
                        Telephone = c.String(maxLength: 20),
                        Fax = c.String(maxLength: 20),
                        Email = c.String(maxLength: 30),
                        Address = c.String(maxLength: 75),
                        Monday = c.String(maxLength: 20),
                        Tuesday = c.String(maxLength: 20),
                        Wednesday = c.String(maxLength: 20),
                        Thursday = c.String(maxLength: 20),
                        Friday = c.String(maxLength: 20),
                        Saturday = c.String(maxLength: 20),
                        Sunday = c.String(maxLength: 20),
                        BannerMessage = c.String(maxLength: 300),
                        BannerColor = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeedBackNavRating = c.Int(nullable: false),
                        FeedBackAccRating = c.Int(nullable: false),
                        FeedBackAppRating = c.Int(nullable: false),
                        FeedBackRecRating = c.Int(nullable: false),
                        FeedBackAvg = c.Double(nullable: false),
                        FeedBackDate = c.DateTime(nullable: false),
                        FeedBackArea = c.String(maxLength: 25),
                        FeedBackComment = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FolderName = c.String(maxLength: 250),
                        RoleName = c.String(maxLength: 100),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HPSFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 250),
                        FileSize = c.Int(nullable: false),
                        FileData = c.Binary(),
                        Thumbnail = c.Binary(),
                        FileContentType = c.String(maxLength: 50),
                        FileDate = c.DateTime(),
                        FileExtension = c.String(maxLength: 5),
                        RoleName = c.String(maxLength: 100),
                        UserId = c.String(maxLength: 128),
                        FolderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Folders", t => t.FolderId, cascadeDelete: true)
                .Index(t => t.FolderId);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgramName = c.String(maxLength: 250),
                        ProgramDescription = c.String(),
                        ProgramGoals = c.String(),
                        ProgramCoordinator = c.String(),
                        ProgramEmail = c.String(maxLength: 50),
                        ProgramPhone = c.String(maxLength: 20),
                        ProgramSchedule = c.String(maxLength: 250),
                        ProgramLocation = c.String(maxLength: 75),
                        ProgramMap = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SlideShowImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlideShowHeading = c.String(maxLength: 50),
                        SlideShowText = c.String(maxLength: 50),
                        SlideShowPosition = c.Int(nullable: false),
                        FileId = c.Int(nullable: false),
                        SlideShowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HPSFiles", t => t.FileId, cascadeDelete: true)
                .ForeignKey("dbo.SlideShows", t => t.SlideShowId, cascadeDelete: true)
                .Index(t => t.FileId)
                .Index(t => t.SlideShowId);
            
            CreateTable(
                "dbo.SlideShows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlideShowName = c.String(maxLength: 50),
                        WebPageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebPages", t => t.WebPageId, cascadeDelete: true)
                .Index(t => t.WebPageId);
            
            CreateTable(
                "dbo.WebPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebPageName = c.String(maxLength: 250),
                        WebPageContent = c.String(),
                        WebPageDescription = c.String(maxLength: 250),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SlideShows", "WebPageId", "dbo.WebPages");
            DropForeignKey("dbo.SlideShowImages", "SlideShowId", "dbo.SlideShows");
            DropForeignKey("dbo.SlideShowImages", "FileId", "dbo.HPSFiles");
            DropForeignKey("dbo.HPSFiles", "FolderId", "dbo.Folders");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Steps", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StepGoals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Minutes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MinuteGoals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.HPSUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CalendarEvents", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Distances", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DistanceGoals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ActivityLogs", "UserActionID", "dbo.UserActions");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.SlideShows", new[] { "WebPageId" });
            DropIndex("dbo.SlideShowImages", new[] { "SlideShowId" });
            DropIndex("dbo.SlideShowImages", new[] { "FileId" });
            DropIndex("dbo.HPSFiles", new[] { "FolderId" });
            DropIndex("dbo.Steps", new[] { "UserId" });
            DropIndex("dbo.StepGoals", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Minutes", new[] { "UserId" });
            DropIndex("dbo.MinuteGoals", new[] { "UserId" });
            DropIndex("dbo.HPSUsers", new[] { "UserId" });
            DropIndex("dbo.CalendarEvents", new[] { "UserId" });
            DropIndex("dbo.Distances", new[] { "UserId" });
            DropIndex("dbo.DistanceGoals", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.ActivityLogs", new[] { "UserActionID" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.WebPages");
            DropTable("dbo.SlideShows");
            DropTable("dbo.SlideShowImages");
            DropTable("dbo.Programs");
            DropTable("dbo.HPSFiles");
            DropTable("dbo.Folders");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.Contacts");
            DropTable("dbo.Steps");
            DropTable("dbo.StepGoals");
            DropTable("dbo.Notifications");
            DropTable("dbo.Minutes");
            DropTable("dbo.MinuteGoals");
            DropTable("dbo.HPSUsers");
            DropTable("dbo.CalendarEvents");
            DropTable("dbo.Distances");
            DropTable("dbo.DistanceGoals");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserActions");
            DropTable("dbo.ActivityLogs");
        }
    }
}
