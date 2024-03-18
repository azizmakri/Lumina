using LuminaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LuminaApp.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(student => student.Parent).WithMany(parent => parent.Students).HasForeignKey(student => student.ParentFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(student => student.Attendances).WithOne(attendance => attendance.Student).HasForeignKey(attendance=>attendance.StudentFK).OnDelete(DeleteBehavior.NoAction);
           // builder.HasMany(student=>student.Evaluations).WithOne(evaluation=>evaluation.Student).HasForeignKey(evaluation=>evaluation.StudentFk).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(teacher=> teacher.Evaluations).WithOne(evaluation=>evaluation.Teacher).HasForeignKey(evaluation=>evaluation.TeacherFk).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(employee => employee.ClassRooms).WithOne(classroom => classroom.employee).HasForeignKey(clasroom => clasroom.employeeFk).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(teacher => teacher.Folders).WithOne(folder => folder.Teacher).HasForeignKey(folder => folder.TeacherFK).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
