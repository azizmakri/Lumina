using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuminaApp.Infrastructure.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<SchoolNews>

    {


        void IEntityTypeConfiguration<SchoolNews>.Configure(EntityTypeBuilder<SchoolNews> builder)
        {
            builder.HasOne(UserAdmin => UserAdmin.School).WithMany(news => news.News).HasForeignKey(news => news.SchoolFk);
        }
    }
}
