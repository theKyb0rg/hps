namespace HPFS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HPFS.Models;
    using System.IO;
    using System.Web.UI.WebControls;
    using System.Drawing;
    using Properties;
    using System.Reflection;
    using HelperMethods;

    internal sealed class Configuration : DbMigrationsConfiguration<HPFS.HPSDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HPFS.HPSDB context)
        {
            // Create instance of role manager and store
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>();
            RoleManager<IdentityRole> roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Create roles if they dont exist
            if (!roleMgr.RoleExists("Administrator"))
            {
                IdentityResult roleResult = roleMgr.Create(new IdentityRole("Administrator"));
            }
            if (!roleMgr.RoleExists("Client"))
            {
                IdentityResult roleResult = roleMgr.Create(new IdentityRole("Client"));
            }
            if (!roleMgr.RoleExists("Board Member"))
            {
                IdentityResult roleResult = roleMgr.Create(new IdentityRole("Board Member"));
            }
            if (!roleMgr.RoleExists("Everyone"))
            {
                IdentityResult roleResult = roleMgr.Create(new IdentityRole("Everyone"));
            }
            if (!roleMgr.RoleExists("Family Association"))
            {
                IdentityResult roleResult = roleMgr.Create(new IdentityRole("Family Association"));
            }

            // Declare UserStore and UserManager
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

            // Declare/create new user and store in manager object in the userstore
            IdentityUser admin = new IdentityUser("admin");
            admin.Email = "admin@admin.admin";

            // Declare/create new user and store in manager object in the userstore
            IdentityUser client = new IdentityUser("client");
            client.Email = "client@client.client";

            // Declare/create new user and store in manager object in the userstore
            IdentityUser director = new IdentityUser("board");
            director.Email = "board@board.board";

            // Declare/create new user and store in manager object in the userstore
            //IdentityUser everyone = new IdentityUser("everyone");
            //everyone.Email = "everyone@everyone.everyone";

            // Declare/create new user and store in manager object in the userstore
            IdentityUser fa = new IdentityUser("family");
            fa.Email = "family@family.family";

            // Store result of user creation
            IdentityResult adminResult = manager.Create(admin, "password");
            IdentityResult clientResult = manager.Create(client, "password");
            IdentityResult directorResult = manager.Create(director, "password");
            //IdentityResult everyoneResult = manager.Create(everyone, "password");
            IdentityResult faResult = manager.Create(fa, "password");

            // Check if user was created and added to role
            if (adminResult.Succeeded)
            {
                manager.AddToRole(admin.Id, "Administrator");
            }
            if (clientResult.Succeeded)
            {
                manager.AddToRole(client.Id, "Client");
            }
            if (directorResult.Succeeded)
            {
                manager.AddToRole(director.Id, "Board Member");
            }
            //if (everyoneResult.Succeeded)
            //{
            //    manager.AddToRole(everyone.Id, "Everyone");
            //}
            if (faResult.Succeeded)
            {
                manager.AddToRole(fa.Id, "Family Association");
            }

            // Add user data to separate table
            context.HPSUsers.AddOrUpdate(
                new HPSUser { Id = 1, FirstName = "The Administrator", LastName = "Person", CreatedOn = DateTime.Now, RoleName = "Administrator", UserId = admin.Id },
                new HPSUser { Id = 2, FirstName = "The Board Member", LastName = "Person", CreatedOn = DateTime.Now, RoleName = "Board Member", UserId = director.Id },
                new HPSUser { Id = 3, FirstName = "The Client", LastName = "Person", CreatedOn = DateTime.Now, RoleName = "Client", UserId = client.Id },
                //new HPSUser { Id = 4, FirstName = "The Everyone", LastName = "Person", CreatedOn = DateTime.Now, RoleName = "Everyone", UserId = everyone.Id },
                new HPSUser { Id = 5, FirstName = "The Family Association", LastName = "Person", CreatedOn = DateTime.Now, RoleName = "Family Association", UserId = fa.Id }
                );

            // Sample seed data for Steps
            context.Steps.AddOrUpdate(
                new Step { Id = 1, StepCount = 0, StepDate = DateTime.Today.AddDays(-28), UserId = admin.Id },
                new Step { Id = 2, StepCount = 0, StepDate = DateTime.Today.AddDays(-28), UserId = client.Id },
                new Step { Id = 3, StepCount = 0, StepDate = DateTime.Today.AddDays(-28), UserId = director.Id },
                new Step { Id = 4, StepCount = 0, StepDate = DateTime.Today.AddDays(-28), UserId = fa.Id }
            );

            // Sample seed data for Distances
            context.Distances.AddOrUpdate(
                new Distance { Id = 1, DistanceCount = 0, DistanceDate = DateTime.Today.AddDays(-28), UserId = admin.Id },
                new Distance { Id = 2, DistanceCount = 0, DistanceDate = DateTime.Today.AddDays(-28), UserId = client.Id },
                new Distance { Id = 3, DistanceCount = 0, DistanceDate = DateTime.Today.AddDays(-28), UserId = director.Id },
                new Distance { Id = 4, DistanceCount = 0, DistanceDate = DateTime.Today.AddDays(-28), UserId = fa.Id }
            );

            // Sample seed data for Minutes
            context.Minutes.AddOrUpdate(
                new Minute { Id = 1, MinuteCount = 0, MinuteDate = DateTime.Today.AddDays(-28), UserId = admin.Id },
                new Minute { Id = 2, MinuteCount = 0, MinuteDate = DateTime.Today.AddDays(-28), UserId = client.Id },
                new Minute { Id = 3, MinuteCount = 0, MinuteDate = DateTime.Today.AddDays(-28), UserId = director.Id },
                new Minute { Id = 4, MinuteCount = 0, MinuteDate = DateTime.Today.AddDays(-28), UserId = fa.Id }
            );

            // File Group Seed Data
            context.Folders.AddOrUpdate(
                new Folder { Id = 1, FolderName = "Slide Show Images", RoleName = "Administrator" },
                new Folder { Id = 2, FolderName = "Client", RoleName = "Client" },
                new Folder { Id = 3, FolderName = "Board Member", RoleName = "Board Member" },
                new Folder { Id = 4, FolderName = "Everyone", RoleName = "Everyone" },
                new Folder { Id = 5, FolderName = "Shared C B", RoleName = "Client, Board Member" },
                new Folder { Id = 6, FolderName = "Shared FA C", RoleName = "Family Association, Client" },
                new Folder { Id = 7, FolderName = "Shared B FA", RoleName = "Board Member, Family Association" }
            );

            // CalendarEvent Seed Data
            context.CalendarEvents.AddOrUpdate(
                new CalendarEvent { Id = 1, CalendarEventName = "Admin Event", CalendarEventDescription = "Only admins can see this event!", CalendarEventDate = DateTime.Now, RoleName = "Administrator" },
                new CalendarEvent { Id = 2, CalendarEventName = "Board Member Event", CalendarEventDescription = "Admins and Board Members can see this event!", CalendarEventDate = DateTime.Now.AddDays(1), RoleName = "Board Member" },
                new CalendarEvent { Id = 3, CalendarEventName = "Family Association Event", CalendarEventDescription = "Admins, Board Members, and Family Assocation can see this event!", CalendarEventDate = DateTime.Now.AddDays(2), RoleName = "Family Association" },
                new CalendarEvent { Id = 4, CalendarEventName = "Client Event", CalendarEventDescription = "Admins, Board Members, Family Association and Clients can see this event!", CalendarEventDate = DateTime.Now.AddDays(3), RoleName = "Client" },
                new CalendarEvent { Id = 5, CalendarEventName = "Public Event", CalendarEventDescription = "Everyone can see this event!", CalendarEventDate = DateTime.Now.AddDays(4), RoleName = "Everyone" }
            );

            // StepGoals Seed Data
            context.StepGoals.AddOrUpdate(
                new StepGoal { Id = 1, GoalSteps = 400000, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = admin.Id },
                new StepGoal { Id = 2, GoalSteps = 400000, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = client.Id },
                new StepGoal { Id = 3, GoalSteps = 400000, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = director.Id },
                new StepGoal { Id = 4, GoalSteps = 400000, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = fa.Id }
            );

            // DistanceGoals Seed Data
            context.DistanceGoals.AddOrUpdate(
                new DistanceGoal { Id = 1, GoalDistance = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = admin.Id },
                new DistanceGoal { Id = 2, GoalDistance = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = client.Id },
                new DistanceGoal { Id = 3, GoalDistance = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = director.Id },
                new DistanceGoal { Id = 4, GoalDistance = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = fa.Id }
            );

            // DistanceGoals Seed Data
            context.MinuteGoals.AddOrUpdate(
                new MinuteGoal { Id = 1, GoalMinute = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = admin.Id },
                new MinuteGoal { Id = 2, GoalMinute = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = client.Id },
                new MinuteGoal { Id = 3, GoalMinute = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = director.Id },
                new MinuteGoal { Id = 4, GoalMinute = 300.0m, GoalStartDate = DateTime.Today, GoalEndDate = DateTime.Today.AddMonths(1), UserId = fa.Id }
            );

            // WebPage Seed Data
            context.WebPages.AddOrUpdate(
                new WebPage { Id = 1, WebPageName = "Programs", WebPageContent = "Content", WebPageDescription = "Description" }
                //new WebPage { Id = 2, WebPageName = "Education And Research", WebPageContent = "Content", WebPageDescription = "Description" },
                //new WebPage { Id = 3, WebPageName = "Rehabilitation And Treatment", WebPageContent = "Content", WebPageDescription = "Description" },
                //new WebPage { Id = 4, WebPageName = "About Us", WebPageContent = "Content", WebPageDescription = "Description" }
            );

            // Create instance of the assembly to access resource data such as images/files etc for seeding
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get all file resources in the resources folder
            string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            // Loop through resources and pluck out the images for seeding
            for (int i = 0; i < resources.Length; i++)
            {
                if (resources[i].ToString().Contains("jpg") || resources[i].ToString().Contains("JPG"))
                {
                    // Get the image stream by Namespace.FileName.Extension
                    Stream imageStream = assembly.GetManifestResourceStream(resources[i].ToString());

                    // Read the binary data from the image
                    Byte[] bytes = StreamHelper.ReadToEnd(imageStream);
                    Byte[] thumbnail = null;

                    // Split the resource at all the periods to get the extension and file name
                    string[] resourceName = resources[i].ToString().Split('.');

                    // Create thumbnail
                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        System.Drawing.Image bmp = System.Drawing.Image.FromStream(memoryStream);
                        thumbnail = HPFS.HelperMethods.UploadFile.ImageToByte(HPFS.HelperMethods.UploadFile.ResizeImage(bmp, 100, 100));
                    }

                    // Add files to files table
                    context.HPSFiles.AddOrUpdate(
                        new HPSFile
                        {
                            Id = i + 1,
                            FileContentType = "image/jpg",
                            FileData = bytes,
                            FileDate = DateTime.Now,
                            FileExtension = resourceName[3],
                            FolderId = 1,
                            FileName = resourceName[2] + "." + resourceName[3],
                            FileSize = bytes.Length,
                            UserId = admin.Id,
                            RoleName = "Administrator",
                            Thumbnail = thumbnail,
                        }
                    );
                }
            }

            // Slide Show Seed Data
            context.SlideShows.AddOrUpdate(
                new SlideShow { Id = 1, SlideShowName = "Collective Kitchen", WebPageId = 1 },
                new SlideShow { Id = 2, SlideShowName = "Computer Tutoring", WebPageId = 1 },
                new SlideShow { Id = 3, SlideShowName = "CottageStudio", WebPageId = 1 },
                new SlideShow { Id = 4, SlideShowName = "Friday Social Group", WebPageId = 1 },
                new SlideShow { Id = 5, SlideShowName = "Gaming Group", WebPageId = 1 },
                new SlideShow { Id = 6, SlideShowName = "Movie Group", WebPageId = 1 },
                new SlideShow { Id = 7, SlideShowName = "Sweet Donation Group", WebPageId = 1 },
                new SlideShow { Id = 8, SlideShowName = "Travelling Cup", WebPageId = 1 },
                new SlideShow { Id = 9, SlideShowName = "Volleyball / Summer Sports", WebPageId = 1 },
                new SlideShow { Id = 10, SlideShowName = "Walking Group", WebPageId = 1 },
                new SlideShow { Id = 11, SlideShowName = "Wednesday Leisure Group", WebPageId = 1 }
                //new SlideShow { Id = 12, SlideShowName = "In Progress", WebPageId = 2 },
                //new SlideShow { Id = 13, SlideShowName = "Current", WebPageId = 2 },
                //new SlideShow { Id = 14, SlideShowName = "Recent", WebPageId = 2 },
                //new SlideShow { Id = 15, SlideShowName = "Links", WebPageId = 2 },
                //new SlideShow { Id = 16, SlideShowName = "Main Content", WebPageId = 3 },
                //new SlideShow { Id = 17, SlideShowName = "Case Management", WebPageId = 3 },
                //new SlideShow { Id = 18, SlideShowName = "Psychiatric Care", WebPageId = 3 },
                //new SlideShow { Id = 19, SlideShowName = "Rehabilitation", WebPageId = 3 },
                //new SlideShow { Id = 20, SlideShowName = "About Us", WebPageId = 4 }
            );

            // Slide Show Images Seed Data
            context.SlideShowImages.AddOrUpdate(

                //Collective Kitchen
                new SlideShowImage { Id = 1, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 1, SlideShowId = (int)SlideShowEnums.CollectiveKitchen },
                new SlideShowImage { Id = 2, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 2, SlideShowId = (int)SlideShowEnums.CollectiveKitchen },
                new SlideShowImage { Id = 3, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 3, SlideShowId = (int)SlideShowEnums.CollectiveKitchen },
                new SlideShowImage { Id = 4, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 4, FileId = 4, SlideShowId = (int)SlideShowEnums.CollectiveKitchen },
                new SlideShowImage { Id = 5, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 5, FileId = 5, SlideShowId = (int)SlideShowEnums.CollectiveKitchen },

                //Computer Tutoring
                new SlideShowImage { Id = 6, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 6, SlideShowId = (int)SlideShowEnums.ComputerTutoring },
                new SlideShowImage { Id = 7, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 7, SlideShowId = (int)SlideShowEnums.ComputerTutoring },
                new SlideShowImage { Id = 8, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 8, SlideShowId = (int)SlideShowEnums.ComputerTutoring },
                new SlideShowImage { Id = 9, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 4, FileId = 9, SlideShowId = (int)SlideShowEnums.ComputerTutoring },
                new SlideShowImage { Id = 10, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 5, FileId = 10, SlideShowId = (int)SlideShowEnums.ComputerTutoring },

                //Cottage Studio
                new SlideShowImage { Id = 11, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 11, SlideShowId = (int)SlideShowEnums.CottageStudio },
                new SlideShowImage { Id = 12, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 12, SlideShowId = (int)SlideShowEnums.CottageStudio },
                new SlideShowImage { Id = 13, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 13, SlideShowId = (int)SlideShowEnums.CottageStudio },
                new SlideShowImage { Id = 14, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 4, FileId = 14, SlideShowId = (int)SlideShowEnums.CottageStudio },
                new SlideShowImage { Id = 15, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 5, FileId = 15, SlideShowId = (int)SlideShowEnums.CottageStudio },

                //Friday Social Club
                new SlideShowImage { Id = 16, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 16, SlideShowId = (int)SlideShowEnums.FridaySocialGroup },
                new SlideShowImage { Id = 17, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 17, SlideShowId = (int)SlideShowEnums.FridaySocialGroup },
                new SlideShowImage { Id = 18, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 18, SlideShowId = (int)SlideShowEnums.FridaySocialGroup },

                //Gaming Group
                new SlideShowImage { Id = 19, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 19, SlideShowId = (int)SlideShowEnums.GamingGroup },
                new SlideShowImage { Id = 20, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 20, SlideShowId = (int)SlideShowEnums.GamingGroup },
                new SlideShowImage { Id = 21, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 21, SlideShowId = (int)SlideShowEnums.GamingGroup },
                new SlideShowImage { Id = 22, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 4, FileId = 22, SlideShowId = (int)SlideShowEnums.GamingGroup },
                new SlideShowImage { Id = 23, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 5, FileId = 23, SlideShowId = (int)SlideShowEnums.GamingGroup },

                //Movie Group
                new SlideShowImage { Id = 24, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 24, SlideShowId = (int)SlideShowEnums.MovieGroup },
                new SlideShowImage { Id = 25, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 25, SlideShowId = (int)SlideShowEnums.MovieGroup },
                new SlideShowImage { Id = 26, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 26, SlideShowId = (int)SlideShowEnums.MovieGroup },

                //Sweet Donations
                new SlideShowImage { Id = 27, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 27, SlideShowId = (int)SlideShowEnums.SweetDonationsGroup },
                new SlideShowImage { Id = 28, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 28, SlideShowId = (int)SlideShowEnums.SweetDonationsGroup },
                new SlideShowImage { Id = 29, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 29, SlideShowId = (int)SlideShowEnums.SweetDonationsGroup },
                new SlideShowImage { Id = 30, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 4, FileId = 30, SlideShowId = (int)SlideShowEnums.SweetDonationsGroup },
                new SlideShowImage { Id = 31, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 5, FileId = 31, SlideShowId = (int)SlideShowEnums.SweetDonationsGroup },

                //Travelling Cup
                new SlideShowImage { Id = 32, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 32, SlideShowId = (int)SlideShowEnums.TravellingCup },
                new SlideShowImage { Id = 33, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 33, SlideShowId = (int)SlideShowEnums.TravellingCup },
                new SlideShowImage { Id = 34, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 34, SlideShowId = (int)SlideShowEnums.TravellingCup },
                new SlideShowImage { Id = 35, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 4, FileId = 35, SlideShowId = (int)SlideShowEnums.TravellingCup },

                //Voleyball Club
                new SlideShowImage { Id = 36, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 36, SlideShowId = (int)SlideShowEnums.VolleyBallSummerSports },
                new SlideShowImage { Id = 37, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 37, SlideShowId = (int)SlideShowEnums.VolleyBallSummerSports },
                new SlideShowImage { Id = 38, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 38, SlideShowId = (int)SlideShowEnums.VolleyBallSummerSports },

                //Walking Group
                new SlideShowImage { Id = 39, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 39, SlideShowId = (int)SlideShowEnums.WalkingGroup },
                new SlideShowImage { Id = 40, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 40, SlideShowId = (int)SlideShowEnums.WalkingGroup },
                new SlideShowImage { Id = 41, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 41, SlideShowId = (int)SlideShowEnums.WalkingGroup },

                //Wednesday Social Club
                new SlideShowImage { Id = 42, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 1, FileId = 16, SlideShowId = (int)SlideShowEnums.WednesdayLeisureGroup },
                new SlideShowImage { Id = 43, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 2, FileId = 17, SlideShowId = (int)SlideShowEnums.WednesdayLeisureGroup },
                new SlideShowImage { Id = 44, SlideShowHeading = "", SlideShowText = "", SlideShowPosition = 3, FileId = 18, SlideShowId = (int)SlideShowEnums.WednesdayLeisureGroup }
            );

            // Notification Seed Data
            //context.Notifications.AddOrUpdate(
            //    new Notification { Id = 1, Title = "This is a test notification Title", Description = "This is a Description for the test Notification be prepared for a lot of words0", Priority = "Normal", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = 1, FileId = 1, UserId = admin.Id },
            //    new Notification { Id = 2, Title = "This is a test notification Title", Description = "This is a Description for the test Notification be prepared for a lot of words1", Priority = "High", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = 1, FileId = 1, UserId = admin.Id },
            //    new Notification { Id = 3, Title = "This is a test notification Title", Description = "This is a Description for the test Notification be prepared for a lot of words2", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = 1, FileId = 1, UserId = admin.Id },
            //    new Notification { Id = 4, Title = "This is a test notification Title", Description = "This is a Description for the test Notification be prepared for a lot of words3", Priority = "Normal", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = 1, FileId = 1, UserId = admin.Id },
            //    new Notification { Id = 5, Title = "This is a test notification Title", Description = "This is a Description for the test Notification be prepared for a lot of words4", Priority = "High", IsRead = true, NotificationDate = DateTime.Now, CalendarEventId = 1, FileId = 1, UserId = admin.Id },
            //    new Notification { Id = 6, Title = "This is a test notification Title", Description = "This is another Description for the test Notification be prepared for a lot of words", Priority = "High", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = admin.Id },
            //    new Notification { Id = 7, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = admin.Id },
            //    new Notification { Id = 8, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 9, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 10, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 11, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 12, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 13, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 14, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 15, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = director.Id },
            //    new Notification { Id = 16, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = client.Id },
            //    new Notification { Id = 17, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = client.Id },
            //    new Notification { Id = 18, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = client.Id },
            //    new Notification { Id = 19, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = client.Id },
            //    new Notification { Id = 20, Title = "This is a test notification Title", Description = "This is yet another Description for the test Notification be prepared for a lot of words", Priority = "Low", IsRead = false, NotificationDate = DateTime.Now, CalendarEventId = null, FileId = null, UserId = client.Id }
            //);

            context.UserActions.AddOrUpdate(
                new UserAction { Id = (int)UserActionEnum.Clicked, Name = "Clicked" },
                new UserAction { Id = (int)UserActionEnum.Navigated, Name = "Navigated" },
                new UserAction { Id = (int)UserActionEnum.Deleted, Name = "Deleted" },
                new UserAction { Id = (int)UserActionEnum.Created, Name = "Created" },
                new UserAction { Id = (int)UserActionEnum.Updated, Name = "Updated" },
                new UserAction { Id = (int)UserActionEnum.Searched, Name = "Searched" },
                new UserAction { Id = (int)UserActionEnum.LoggedIn, Name = "LoggedIn" },
                new UserAction { Id = (int)UserActionEnum.Downloaded, Name = "Downloaded" }

            );

            // Contact Seed Data (only needs one entry for updating)
            context.Contacts.AddOrUpdate(
                new Contact { Id = 1, Address = "20 Hughson St S, Suite 405, Hamilton, Ontario L8N 2A1", Company = "Hamilton Program for Schizophrenia", Email = "info@hpfs.on.ca", Fax = "(905) 546-0055", Telephone = "905-525-2832", Monday = "9 AM - 5 PM", Tuesday = "9 AM - 5 PM", Wednesday = "9 AM - 5 PM", Thursday = "9 AM - 5 PM", Friday = "9 AM - 5 PM", Saturday = "Closed", Sunday = "Closed", BannerMessage = "This msg can be changed in admin settings. If you set an empty message, it will go away!", BannerColor = "#ffffff"});

            // Feedback Seed Data
                //    context.Feedbacks.AddOrUpdate(
                //new FeedBack { Id = 1, FeedBackNavRating = 1, FeedBackAccRating = 5, FeedBackAppRating = 1, FeedBackComment = "Very Well Done! I really enjoyed everything about your site.", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 2, FeedBackArea = "Education" },
                //new FeedBack { Id = 2, FeedBackNavRating = 2, FeedBackAccRating = 4, FeedBackAppRating = 2, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 1, FeedBackArea = "Contact" },
                //new FeedBack { Id = 3, FeedBackNavRating = 3, FeedBackAccRating = 3, FeedBackAppRating = 3, FeedBackComment = "Very Well Done! I really enjoyed everything about your site.", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 2, FeedBackArea = "Overall" },
                //new FeedBack { Id = 4, FeedBackNavRating = 4, FeedBackAccRating = 2, FeedBackAppRating = 4, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 3, FeedBackArea = "Other" },
                //new FeedBack { Id = 5, FeedBackNavRating = 5, FeedBackAccRating = 1, FeedBackAppRating = 5, FeedBackComment = "I've seen better!... I've seen better?", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 4, FeedBackArea = "Rehabilitation" },
                //new FeedBack { Id = 6, FeedBackNavRating = 4, FeedBackAccRating = 5, FeedBackAppRating = 4, FeedBackComment = "I've seen better!... I've seen better ?", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 5, FeedBackArea = "Education" },
                //new FeedBack { Id = 7, FeedBackNavRating = 3, FeedBackAccRating = 4, FeedBackAppRating = 3, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 4, FeedBackArea = "Dashboard" },
                //new FeedBack { Id = 8, FeedBackNavRating = 2, FeedBackAccRating = 3, FeedBackAppRating = 2, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 3, FeedBackArea = "Education" },
                //new FeedBack { Id = 9, FeedBackNavRating = 1, FeedBackAccRating = 2, FeedBackAppRating = 1, FeedBackComment = "Terrible job on the Appearance. Needs some work.", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 2, FeedBackArea = "About" },
                //new FeedBack { Id = 10, FeedBackNavRating = 1, FeedBackAccRating = 1, FeedBackAppRating = 5, FeedBackComment = "Accessibility could use some work.", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 1, FeedBackArea = "Education" },
                //new FeedBack { Id = 11, FeedBackNavRating = 2, FeedBackAccRating = 2, FeedBackAppRating = 4, FeedBackComment = "The navigation wasn't the greatest but I liked how it looked.", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 2, FeedBackArea = "Education" },
                //new FeedBack { Id = 12, FeedBackNavRating = 3, FeedBackAccRating = 3, FeedBackAppRating = 3, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 3, FeedBackArea = "Home" },
                //new FeedBack { Id = 13, FeedBackNavRating = 4, FeedBackAccRating = 4, FeedBackAppRating = 2, FeedBackComment = "Good job!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 4, FeedBackArea = "Education" },
                //new FeedBack { Id = 14, FeedBackNavRating = 5, FeedBackAccRating = 5, FeedBackAppRating = 1, FeedBackComment = "I've seen better!... I've seen better?", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 5, FeedBackArea = "Education" },
                //new FeedBack { Id = 15, FeedBackNavRating = 4, FeedBackAccRating = 4, FeedBackAppRating = 2, FeedBackComment = "Pssh Terrible job on the Appearance. Needs some work.", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 4, FeedBackArea = "Education" },
                //new FeedBack { Id = 16, FeedBackNavRating = 3, FeedBackAccRating = 3, FeedBackAppRating = 3, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 3, FeedBackArea = "LandingPage" },
                //new FeedBack { Id = 17, FeedBackNavRating = 2, FeedBackAccRating = 2, FeedBackAppRating = 4, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 2, FeedBackArea = "PublicEvents" },
                //new FeedBack { Id = 18, FeedBackNavRating = 1, FeedBackAccRating = 1, FeedBackAppRating = 5, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 1, FeedBackArea = "Education" },
                //new FeedBack { Id = 19, FeedBackNavRating = 2, FeedBackAccRating = 2, FeedBackAppRating = 4, FeedBackComment = "Well done!", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 2, FeedBackArea = "Settings" },
                //new FeedBack { Id = 20, FeedBackNavRating = 1, FeedBackAccRating = 1, FeedBackAppRating = 1, FeedBackComment = "Brutal, absolute garbage. I would not pay for this piece of crap.", FeedBackDate = DateTime.Now.Date, FeedBackRecRating = 1, FeedBackArea = "Overall" });

            // Program Seed Data (only needs one for each program to update)
            context.Programs.AddOrUpdate(
                new Program
                {
                    Id = 1,
                    ProgramName = "Collective Kitchen",
                    ProgramDescription = "The Collective Kitchen allows HPS clients to pool their money and their commitment to cook nutritious meals together.",
                    ProgramGoals = "Providing several healthy meals each week this program has proven to reduce shopping costs, increase socialization and develops culinary skills.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramSchedule = "Monday Rotations<br /><br />Planning Week: 1:00PM - 2:00PM<br /><br />Cooking Week: 1:00PM - 4:00PM",
                    ProgramLocation = "20 Hughson St. S. Hamilton, ON L8N 2A1 Suite #405 (Kitchen)",
                    ProgramPhone = "905-525-2832",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231"
                },

                new Program
                {
                    Id = 2,
                    ProgramName = "Computer Tutoring",
                    ProgramDescription = "Computer Tutoring is a program that allows client to use the client computer room located in HPS.",
                    ProgramGoals = "Available to HPS clients looking to learn basic computer and Internet skills through the use of an HPS peer tutor.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "N/A",
                    ProgramLocation = "20 Hughson St. S. Hamilton, ON L8N 2A1 Suite #405 (Computer Room)",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231"
                },

                new Program
                {
                    Id = 3,
                    ProgramName = "Cottage Studio",
                    ProgramDescription = "The Cottage Studio is a unique arts program for people recovering from any mental illness and supported by Hamilton Program For Schizophrenia and the Hamilton Program For Schizophrenia Family Association.",
                    ProgramGoals = "The aim of the Cottage Studio is to promote mental health and artistic potential. Members are offered complimentary space and art supplies in a relaxed social environment.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Monday – Friday<br /><br />1:00PM - 3:00PM",
                    ProgramLocation = "70 James Street S. Hamilton, ON L8P 2Y8",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2905.854086558646!2d-79.87223068419927!3d43.254479786138944!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b9cb3e7f337%3A0xac9ac6600bfc6090!2s70+James+St+S%2C+Hamilton%2C+ON+L8P!5e0!3m2!1sen!2sca!4v1458079236022"
                },

                new Program
                {
                    Id = 4,
                    ProgramName = "Friday Social Group",
                    ProgramDescription = "Held every Friday here on site at HPS, clients come to sit down and talk with each other while enjoying a drink and a snack.",
                    ProgramGoals = "This gives clients an opportunity to meet new people and increase their socializing skills.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Fridays<br /><br />1:15PM - 2:15PM",
                    ProgramLocation = "20 Hughson St. S. Hamilton, ON L8N 2A1 Suite #405 (Social Recreation Room)",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231"
                },

                new Program
                {
                    Id = 5,
                    ProgramName = "Gaming Group",
                    ProgramDescription = "The Gaming Group enables clients to participate with an interactive gaming system where they use their various body parts as the remotes.",
                    ProgramGoals = "This group promotes healthy living by exercising. Gaming Group runs Wednesday afternoon before Wednesday Social Group.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Wednesdays<br /><br />2:00PM - 3:00PM",
                    ProgramLocation = "20 Hughson St. S. Hamilton, ON L8N 2A1 Suite #405 (Social Recreation Room)",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231"
                },

                new Program
                {
                    Id = 6,
                    ProgramName = "Movie Group",
                    ProgramDescription = "The Movie Group gives clients an opportunity to watch movies together every Thursday afternoon while getting to know other HPS clients. Every month, the group collectively decides on the movies they wish to watch for the next month.",
                    ProgramGoals = "To engage clients in a group activity and an opportunity to demonstrate current cinematic features.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Thursdays<br /><br />2:00PM - 4:00PM",
                    ProgramLocation = "20 Hughson St. S. Hamilton, ON L8N 2A1 Suite #405 (Social Recreation Room)",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231"
                },

                new Program
                {
                    Id = 7,
                    ProgramName = "Sweet Donations Group",
                    ProgramDescription = "Sweet Donations is a weekly baking group with goals to improve food preparation skills in the kitchen.",
                    ProgramGoals = "To increase opportunities for socialization and to contribute to the Hamilton community through the donation of baked goods to various local community organizations.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Tuesday Rotations<br /><br />Planning Week: 1:30PM - 2:30PM<br /><br />Baking Week: 1:30PM - 4:30PM",
                    ProgramLocation = "20 Hughson St. S. Hamilton, ON L8N 2A1 Suite #405 (Kitchen)",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231"
                },

                new Program
                {
                    Id = 8,
                    ProgramName = "Travelling Cup",
                    ProgramDescription = "Every Monday afternoon, HPS clients get together to walk to local coffee shops to experience different beverages and to socialize.",
                    ProgramGoals = "To encourage group activity and promote socializing within the Hamilton community. Clients are able to experience various coffee shops within their own community.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Mondays<br /><br />3:00PM - 4:00PM",
                    ProgramLocation = "Hamilton Community (Coffee Shops)",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d46505.2171937309!2d-79.88148484080853!3d43.23934703738963!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1sHamilton+coffe+shops!5e0!3m2!1sen!2sca!4v1458079880344"
                },

                new Program
                {
                    Id = 9,
                    ProgramName = "Volleyball / SummerSports",
                    ProgramDescription = "Every Friday, HPS clients get together with clients from another mental health agency to play volleyball at a nearby recreation center.",
                    ProgramGoals = "It is friendly competition which helps prepare the clients for the Volleyball tournament in May hosted by St. Joseph’s Healthcare at their mountain site. It also helps to promote healthy physical activity.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Fridays<br /><br />10:00AM - 11:00AM<br /><br />This program does not run in the summer months June - August.",
                    ProgramLocation = "Central Memorial Recreation Centre 93 West Avenue S.Hamilton, ON L8N 1S1",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2906.0928704263256!2d-79.86170568419942!3d43.24947538646016!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b9134e0b6a9%3A0xaec09ca01f775a6!2sCentral+Memorial+Recreation+Centre!5e0!3m2!1sen!2sca!4v1458080255010"
                },

                new Program
                {
                    Id = 10,
                    ProgramName = "Walking Group",
                    ProgramDescription = "The Walking Group combines both socialization and exercise while heading out for extensive walks throughout Hamilton every Wednesday morning.",
                    ProgramGoals = "To promote a healthy and active lifestyle and encouraging physical activity and socialization.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Wednesdays<br /><br />10:00AM - 11:00AM",
                    ProgramLocation = "Hamilton Community",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d185954.82478229573!2d-80.07551560781006!3d43.26097496915362!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c986c27de778f%3A0x2b6aee56d8df0e21!2sHamilton%2C+ON!5e0!3m2!1sen!2sca!4v1458079748503"
                },

                new Program
                {
                    Id = 11,
                    ProgramName = "Wednesday Leisure Group",
                    ProgramDescription = "Wednesday Social Group is an activity based group that allows HPS clients to participate in craft making, playing games and various other activities. It is an afternoon group and runs every Wednesday on site at HPS.",
                    ProgramGoals = "To allow clients to evolve their creativity, as well as socializing within a group setting and to connect through various activities.",
                    ProgramCoordinator = "HPS",
                    ProgramEmail = "info@hpfs.on.ca",
                    ProgramPhone = "905-525-2832",
                    ProgramSchedule = "Wednesdays<br /><br />3:30PM - 4:30PM",
                    ProgramLocation = "20 Hughson St. S. Hamilton, ON L8N 2A1 Suite #405 (Kitchen)",
                    ProgramMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231"

                });
        }
    }
}
