using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTracker.Common;

namespace BBTracker.Persistence.Configurations
{
    class PlayConfiguration : IEntityTypeConfiguration<Play>
    {
        public void Configure(EntityTypeBuilder<Play> builder)
        {
            builder.ToTable("Plays");
            builder.HasKey(p => p.Id);
            builder.HasDiscriminator<PlayTypeEnum>("PlayType")
                .HasValue<Assist>(PlayTypeEnum.Assist)
                .HasValue<Block>(PlayTypeEnum.Block)
                .HasValue<FieldGoal>(PlayTypeEnum.FieldGoal)
                .HasValue<Rebound>(PlayTypeEnum.Rebound)
                .HasValue<Steal>(PlayTypeEnum.Steal)
                .HasValue<Substitution>(PlayTypeEnum.Substitution)
                .HasValue<Turnover>(PlayTypeEnum.Turnover)
                .HasValue<Foul>(PlayTypeEnum.Foul)
                .HasValue<Play>(PlayTypeEnum.Other);
            builder.Property(p => p.Time)
                .IsRequired();
            builder.HasOne(p => p.Game)
                .WithMany(g => g.Plays)
                .HasForeignKey(p => p.GameId);
            builder.HasOne(p => p.Player)
                .WithMany(p => p.Plays)
                .HasForeignKey(p => p.PlayerId);
            builder.Property(p => p.GameTime)
                .IsRequired();
            builder.Property(p => p.GameTime)
                .IsRequired();
        }
    }
}
