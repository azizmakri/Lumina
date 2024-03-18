using LuminaApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure
{
    public class LuminaAppContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected LuminaAppContext()
        {
        }
        public LuminaAppContext(DbContextOptions<LuminaAppContext> options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
        .HasOne(a => a.attendance)
        .WithOne(s => s.session)
        .HasForeignKey<Attendance>(s => s.SessionFK);

            modelBuilder.Entity<User>()
        .HasOne(a => a.history)
        .WithOne(s => s.user)
        .HasForeignKey<User>(s => s.historyFK);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuminaAppContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }


    }
}
