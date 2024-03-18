using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LuminaApp.Domain.Entities;

namespace LuminaApp.Infrastructure.Configurations
{
    public class SeanceConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            
            builder.HasOne(s => s.Teacher)
                .WithMany(e => e.sessions)
                .HasForeignKey(s => s.TeacherFK);
            builder.HasOne(s => s.subject)
                .WithMany(m => m.sessions)
                .HasForeignKey(s => s.SubjectFK);
      //      builder.HasOne(session => session.attendance).WithOne(attendance => attendance.session);
        }
    }
}
