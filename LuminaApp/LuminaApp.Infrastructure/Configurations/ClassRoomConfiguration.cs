using LuminaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuminaApp.Infrastructure.Configurations
{
    public class ClassRoomConfiguration : IEntityTypeConfiguration<ClassRoom>
    {
        public void Configure(EntityTypeBuilder<ClassRoom> builder)
        {
            builder.HasMany(classroom => classroom.equipments).WithOne(equipement => equipement.classRoom).HasForeignKey(equipement=>equipement.classRoomFK);
        }
    }
}
