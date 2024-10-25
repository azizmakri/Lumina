using LuminaGed.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Infrastructure.Configurations
{
    public class FolderConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.HasMany(folder => folder.Documents).WithOne(document => document.folder).HasForeignKey(document => document.folderFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(folder => folder.ParentFolder).WithMany(parentfolder => parentfolder.Folders).HasForeignKey(folder => folder.ParenFolderFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(folder => folder.Teacher).WithMany(teacher => teacher.Folders).HasForeignKey(folder => folder.TeacherFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(folder => folder.grade).WithMany(grade => grade.Folders).HasForeignKey(grade => grade.GradeFK).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
