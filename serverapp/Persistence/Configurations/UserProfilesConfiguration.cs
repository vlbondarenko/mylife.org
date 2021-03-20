using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ApplicationCore.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    class UserProfilesConfiguration:IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.FirstName).HasColumnType("varchar(15)");
            builder.Property(e => e.LastName).HasColumnType("varchar(15)");
            builder.Property(e => e.City).HasColumnType("varchar(15)");
            builder.Property(e => e.Country).HasColumnType("varchar(15)");
            builder.Property(e => e.BirthDate).HasColumnType("timestamp");
        }
    }
}
