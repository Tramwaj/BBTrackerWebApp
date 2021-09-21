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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.OfficialName).IsRequired();
            builder.Property(g => g.City).IsRequired();
            builder.Property(g => g.Street).IsRequired();
        }
    }
}
