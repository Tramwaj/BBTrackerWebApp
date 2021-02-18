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
    class ReboundConfiguration : IEntityTypeConfiguration<Rebound>
    {
        public void Configure(EntityTypeBuilder<Rebound> builder)
        {
            builder.Property(r => r.IsOffensive)
                      .IsRequired();
        }
    }
}
