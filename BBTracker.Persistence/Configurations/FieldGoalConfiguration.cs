using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Persistence.Configurations
{
    public class FieldGoalConfiguration : IEntityTypeConfiguration<FieldGoal>
    {
        public void Configure(EntityTypeBuilder<FieldGoal> builder)
        {
            builder.Property(fg => fg.Points).IsRequired();
            builder.Property(fg => fg.Made).IsRequired();
            builder.Property(fg => fg.WasAssisted).IsRequired();
            builder.Property(fg => fg.WasBlocked).IsRequired();
        }
    }
}
