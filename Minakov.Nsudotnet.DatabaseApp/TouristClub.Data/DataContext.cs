using System.Data.Entity;
using TouristClub.Data.Entity;
using TouristClub.Data.Properties;

namespace TouristClub.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base(Resources.ConnectingString)
        {
        }

        public DataContext(string connectingString): base(Resources.ConnectingString)
        {
        }

        public DbSet<Category> CategorySet { get; set; }
        public DbSet<PersonalData> PersonalDataSet { get; set; }
        public DbSet<Head> HeadSet { get; set; }
        public DbSet<Section> SectionSet { get; set; }
        public DbSet<Traning> TraningSet { get; set; }
        public DbSet<Sportsman> SportsmanSet { get; set; }
        public DbSet<Trainer> TrainerSet { get; set; }
        public DbSet<Group> GroupSet { get; set; }
        public DbSet<Tourist> TouristSet { get; set; }
        public DbSet<Competition> CompetitionSet { get; set; }
        public DbSet<CampaignType> CampaignTypeSet { get; set; }
        public DbSet<Diary> DiarySet { get; set; }
        public DbSet<Stop> StopSet { get; set; }
        public DbSet<RoutePoint> RoutePointSet { get; set; }
        public DbSet<Campaign> CampaignSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>()
                .HasMany(e => e.RoutePoint)
                .WithMany(e => e.Campaign);

            modelBuilder.Entity<Campaign>()
                .HasMany(e => e.PersonalData)
                .WithMany(e => e.Campaign);

            modelBuilder.Entity<Campaign>()
                .HasRequired(e => e.CampaignType);
            modelBuilder.Entity<Campaign>()
                .HasRequired(e => e.Category);
            modelBuilder.Entity<Campaign>()
                .HasRequired(e => e.Diary);
            modelBuilder.Entity<Campaign>()
                .HasRequired(e => e.Sportsman);

            modelBuilder.Entity<Campaign>().Property(u => u.DiaryId).IsOptional();
            modelBuilder.Entity<Campaign>().Property(e => e.CategoryId).IsOptional();
            modelBuilder.Entity<Campaign>().Property(e => e.SportsmanId).IsOptional();

            modelBuilder.Entity<CampaignType>()
                .HasMany(e => e.Campaign)
                .WithRequired(e => e.CampaignType)
                .HasForeignKey(e => e.CampaignTypeId)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<CampaignType>()
                .HasMany(e => e.Section)
                .WithMany(e => e.CampaignType);

            modelBuilder.Entity<CampaignType>().Property(u => u.Name).HasMaxLength(30);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Campaign)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Sportsman)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Competition>()
                .HasMany(e => e.Sportsman)
                .WithMany(e => e.Competition);

            modelBuilder.Entity<Competition>().Property(u => u.Name).HasMaxLength(30);

            modelBuilder.Entity<Diary>()
                .HasMany(e => e.Campaign)
                .WithRequired(e => e.Diary)
                .HasForeignKey(e => e.DiaryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Diary>()
                .HasMany(e => e.Stop)
                .WithRequired(e => e.Diary)
                .HasForeignKey(e => e.DiaryId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Diary>().Property(u => u.Name).HasMaxLength(30);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Tourist)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasRequired(e => e.Trainer);

            modelBuilder.Entity<Group>()
                .HasRequired(e => e.Section);

            modelBuilder.Entity<Group>().Property(u => u.Name).HasMaxLength(30);
            modelBuilder.Entity<Group>().Property(e => e.TrainerId).IsOptional();

            modelBuilder.Entity<Head>()
                .HasRequired(e => e.Section);

            modelBuilder.Entity<Head>()
                .HasRequired(e => e.PersonalData);

            modelBuilder.Entity<PersonalData>()
                .HasMany(e => e.Head)
                .WithRequired(e => e.PersonalData)
                .HasForeignKey(e => e.PersonalDataId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PersonalData>()
                .HasMany(e => e.Sportsman)
                .WithRequired(e => e.PersonalData)
                .HasForeignKey(e => e.PersonalDataId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PersonalData>()
                .HasMany(e => e.Tourist)
                .WithRequired(e => e.PersonalData)
                .HasForeignKey(e => e.PersonalDataId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PersonalData>()
                .HasMany(e => e.Campaign)
                .WithMany(e => e.PersonalData);

            modelBuilder.Entity<PersonalData>().Property(u => u.Name).HasMaxLength(30);
            modelBuilder.Entity<PersonalData>().Property(u => u.Surname).HasMaxLength(30);
            modelBuilder.Entity<PersonalData>().Property(u => u.Patronymic).HasMaxLength(30);
            modelBuilder.Entity<PersonalData>().Property(u => u.Gender).HasMaxLength(10);

            modelBuilder.Entity<RoutePoint>()
                .HasMany(e => e.Stop)
                .WithRequired(e => e.RoutePoint)
                .HasForeignKey(e => e.RoutePointId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoutePoint>()
                .HasMany(e => e.Campaign)
                .WithMany(e => e.RoutePoint);

            modelBuilder.Entity<RoutePoint>().Property(u => u.Name).HasMaxLength(30);

            modelBuilder.Entity<Section>()
                 .HasMany(e => e.Sportsman)
                 .WithRequired(e => e.Section)
                 .HasForeignKey(e => e.SectionId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.CampaignType)
                .WithMany(e => e.Section);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.Head)
                .WithRequired(e => e.Section)
                .HasForeignKey(e => e.SectionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.Group)
                .WithRequired(e => e.Section)
                .HasForeignKey(e => e.SectionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Section>().Property(u => u.Name).HasMaxLength(30);

            modelBuilder.Entity<Sportsman>()
                .HasMany(e => e.Trainer)
                .WithRequired(e => e.Sportsman)
                .HasForeignKey(e => e.SportsmanId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Sportsman>()
                .HasMany(e => e.Competition)
                .WithMany(e => e.Sportsman);

            modelBuilder.Entity<Sportsman>()
                .HasMany(e => e.Campaign)
                .WithRequired(e => e.Sportsman)
                .HasForeignKey(e => e.SportsmanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sportsman>()
                .HasRequired(e => e.Section);

            modelBuilder.Entity<Sportsman>()
                .HasRequired(e => e.Category);

            modelBuilder.Entity<Sportsman>()
                .HasRequired(e => e.PersonalData);

            modelBuilder.Entity<Stop>()
                .HasRequired(e => e.Diary);

            modelBuilder.Entity<Stop>()
                .HasRequired(e => e.RoutePoint);

            modelBuilder.Entity<Tourist>()
                .HasRequired(e => e.Group);

            modelBuilder.Entity<Tourist>()
                .HasRequired(e => e.PersonalData);

            modelBuilder.Entity<Trainer>()
                .HasMany(e => e.Traning)
                .WithRequired(e => e.Trainer)
                .HasForeignKey(e => e.TrainerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trainer>()
                .HasMany(e => e.Group)
                .WithRequired(e => e.Trainer)
                .HasForeignKey(e => e.TrainerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trainer>()
                .HasRequired(e => e.Sportsman);

            modelBuilder.Entity<Traning>()
                .HasRequired(e => e.Trainer);

            modelBuilder.Entity<Traning>().Property(u => u.Place).HasMaxLength(30);
        }
    }
}