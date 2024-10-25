using LuminaGed.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace LuminaGed.Infrastructure
{
   
   public class LuminaGedContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
     
        public DbSet<Folder> Folders { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected LuminaGedContext()
        {
        }
        public LuminaGedContext(DbContextOptions<LuminaGedContext> options) : base(options)
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

          
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuminaGedContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }


    }
}
