using LuminaApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LuminaApp.Infrastructure
{
    public class LuminaAppContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SchoolNews> SchoolNews  { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        protected LuminaAppContext()
        {
        }
        public LuminaAppContext(DbContextOptions<LuminaAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Enable lazy loading
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>()
        .HasOne(a => a.history)
        .WithOne(s => s.user)
        .HasForeignKey<User>(s => s.historyFK);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuminaAppContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NotificationEntity>()
        .HasOne(e => e.schoolnews)
            .WithOne(e => e.notification)
        .HasForeignKey<NotificationEntity>(e => e.newsFk);

        }


    }
}
