using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DataAccess.EntityFramework
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> modelBuilder)
        {
            modelBuilder.ToTable("Manufacturer");

            modelBuilder.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(15);

            modelBuilder.Property(e => e.ManufacturerLogo)
                    .IsRequired();
        }
    }
}
