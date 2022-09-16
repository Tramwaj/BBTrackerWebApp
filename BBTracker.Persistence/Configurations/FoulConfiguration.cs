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
    internal class FoulConfiguration : IEntityTypeConfiguration<Foul>
    {        public void Configure(EntityTypeBuilder<Foul> builder)
        {
            //builder.Property(f=>f.FouledPlayer).IsRequired();
        }
    }
}
