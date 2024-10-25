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
    public class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder.HasOne(e=>e.session).WithMany(s=>s.evaluations).HasForeignKey(e=>e.sessionFk);
            builder.HasOne(e=>e.student).WithMany(s=>s.evaluations).HasForeignKey(e=>e.studentFk);
        }
    }
}
