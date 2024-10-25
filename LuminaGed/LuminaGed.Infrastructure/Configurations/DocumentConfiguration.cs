using LuminaGed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Infrastructure.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.HasOne(document=>document.folder).WithMany(folder=>folder.Documents).HasForeignKey(doc=>doc.folderFK);
            builder.HasOne(document=>document.student).WithMany(student => student.studentDocuments).HasForeignKey(doc=>doc.studentFK);
            builder.HasOne(document=>document.teacher).WithMany(teacher => teacher.teacherDocuments).HasForeignKey(doc=>doc.teacherFK);
        }
    }
}
