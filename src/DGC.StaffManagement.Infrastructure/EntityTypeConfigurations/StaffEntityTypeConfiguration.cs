using DGC.StaffManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGC.Staff_Management.Infrastructure.EntityTypeConfigurations
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(125);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x=> x.Gender).HasConversion<string> ().IsRequired();
        }
    }
}
