using Lumina.Authentification.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Authentification.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(student => student.Parent).WithMany(parent => parent.Students).HasForeignKey(student => student.ParentFK).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
