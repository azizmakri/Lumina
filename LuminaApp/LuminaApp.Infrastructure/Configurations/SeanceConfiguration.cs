using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LuminaApp.Domain.Entities;

namespace LuminaApp.Infrastructure.Configurations
{
    public class SeanceConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasOne(s => s.subject)
                .WithMany(m => m.sessions)
                .HasForeignKey(s => s.SubjectFK)
                .OnDelete(DeleteBehavior.Cascade); // Changed to Cascade

            builder.HasMany(session => session.attendance)
                .WithOne(attendance => attendance.session)
                .HasForeignKey(att => att.SessionFK)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(session => session.ClassRoom)
                .WithMany(classroom => classroom.Session)
                .HasForeignKey(session => session.ClassRoomFK)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(s => s.evaluations)
                .WithOne(evaluation => evaluation.session)
                .HasForeignKey(e => e.sessionFk)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
