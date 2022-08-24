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
                .HasValue<Assist>(PlayTypeEnum.assist)
                .HasValue<Block>(PlayTypeEnum.block)
                .HasValue<FieldGoal>(PlayTypeEnum.fieldgoal)
                .HasValue<Rebound>(PlayTypeEnum.rebound)
                .HasValue<Steal>(PlayTypeEnum.steal)
                .HasValue<Substitution>(PlayTypeEnum.substitution)
                .HasValue<Turnover>(PlayTypeEnum.turnover)
                .HasValue<Foul>(PlayTypeEnum.foul);
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
            builder.Property(p => p.IsTeamB)
                .IsRequired();
        }
    }
}
