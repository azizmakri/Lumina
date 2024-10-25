using LuminaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuminaApp.Infrastructure.Configurations
{
    public class FolderConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.HasMany(folder => folder.Documents).WithOne(document => document.folder).HasForeignKey(document=>document.folderFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(folder => folder.ParentFolder).WithMany(parentfolder => parentfolder.Folders).HasForeignKey(folder=>folder.ParenFolderFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(folder => folder.grade).WithMany(grade => grade.Folders).HasForeignKey(grade => grade.GradeFK).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
