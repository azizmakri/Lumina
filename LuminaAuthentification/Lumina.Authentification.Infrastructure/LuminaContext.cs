using Lumina.Authentification.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Infrastructure
{
    public class LuminaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }


        public LuminaContext(DbContextOptions<LuminaContext> options) : base(options)

        {

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuminaContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }
    }
}
