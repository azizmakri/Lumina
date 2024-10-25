using LuminaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne(s => s.grade)
                .WithMany(e => e.subjects)
                .HasForeignKey(e => e.gradeFK)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.teacher)
                .WithMany(e => e.subjects)
                .HasForeignKey(e => e.teacherFK)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
