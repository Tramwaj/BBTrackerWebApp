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
    class SubstitutionConfiguration : IEntityTypeConfiguration<Substitution>
    {
        public void Configure(EntityTypeBuilder<Substitution> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Game)
                .WithMany(g => g.Substitutions)
                .HasForeignKey(s => s.GameId);

            builder.HasOne(s => s.Player)
                .WithMany(p => p.Substitutions)
                .HasForeignKey(s => s.PlayerId);

            builder.Property(s => s.Time).IsRequired();
            builder.Property(s => s.SubbedIn).IsRequired();

        }
    }
}
