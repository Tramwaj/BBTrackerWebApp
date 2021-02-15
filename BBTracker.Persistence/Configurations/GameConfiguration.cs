using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BBTracker.Persistence.Configurations
{
    class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Start).IsRequired();
            builder.HasOne(g => g.Owner)
                .WithMany(u => u.OwnedGames)
                .HasForeignKey(g => g.OwnerId);
        }
    }
}
