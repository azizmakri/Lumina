using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LuminaApp.Domain.Entities;


namespace LuminaApp.Infrastructure.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.HasOne(document => document.folder).WithMany(folder => folder.Documents).HasForeignKey(doc => doc.folderFK);
            builder.HasOne(document => document.student).WithMany(student => student.studentDocuments).HasForeignKey(doc => doc.studentFK);
            builder.HasOne(document => document.teacher).WithMany(teacher => teacher.teacherDocuments).HasForeignKey(doc => doc.teacherFK);
        }
    }
}
