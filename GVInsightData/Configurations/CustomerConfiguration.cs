﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.Models;

namespace GVInsightData.Configurations
{
    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.Name).IsRequired().HasMaxLength(50);

            //builder
            //    .HasOne(m => m.Artist)
            //    .WithMany(a => a.Musics)
            //    .HasForeignKey(m => m.ArtistId);

            builder.ToTable("Customer");
        }
    }
}
