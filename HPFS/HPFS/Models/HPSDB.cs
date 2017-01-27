namespace HPFS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;
    public partial class HPSDB : DbContext
    {
        public HPSDB()
            : base("name=HPSDB")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<HPSFile> HPSFiles { get; set; }
        public virtual DbSet<WebPage> WebPages { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Distance> Distances { get; set; }
        public virtual DbSet<StepGoal> StepGoals { get; set; }
        public virtual DbSet<DistanceGoal> DistanceGoals { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<CalendarEvent> CalendarEvents { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<SlideShow> SlideShows { get; set; }
        public virtual DbSet<SlideShowImage> SlideShowImages { get; set; }
        public virtual DbSet<HPSUser> HPSUsers { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<FeedBack> Feedbacks { get; set; }
        public virtual DbSet<Minute> Minutes { get; set; }
        public virtual DbSet<MinuteGoal> MinuteGoals { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<UserAction> UserActions { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);

            // Delete all associated records with a user when a user is deleted
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Steps)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.StepGoals)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Distances)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
               .HasMany(e => e.DistanceGoals)
               .WithRequired(e => e.AspNetUser)
               .HasForeignKey(e => e.UserId)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
               .HasMany(e => e.HPSUsers)
               .WithRequired(e => e.AspNetUser)
               .HasForeignKey(e => e.UserId)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
               .HasMany(e => e.Notifications)
               .WithRequired(e => e.AspNetUser)
               .HasForeignKey(e => e.UserId)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Minutes)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AspNetUser>()
               .HasMany(e => e.MinuteGoals)
               .WithRequired(e => e.AspNetUser)
               .HasForeignKey(e => e.UserId)
               .WillCascadeOnDelete(true);

            // Set cascade delete on files when a folder is deleted
            modelBuilder.Entity<Folder>()
               .HasMany(e => e.Files)
               .WithRequired(e => e.Folder)
               .HasForeignKey(e => e.FolderId)
               .WillCascadeOnDelete(true);
        }
    }
}
