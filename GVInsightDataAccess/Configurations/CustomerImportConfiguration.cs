using System;
using System.Collections.Generic;
using System.Text;
using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVInsightDataAccess.Configurations
{
   public class CustomerImportConfiguration : IEntityTypeConfiguration<CustomerImport>
    {
        public void Configure(EntityTypeBuilder<CustomerImport> builder)
        {
            builder
                 .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder
               .Property(m => m.LastImport)
               .IsRequired() ;

            builder
                .HasOne(m => m.Customer)
                .WithMany(a => a.CustomerImports)
                .HasForeignKey(m => m.CustomerId);

            builder
                .ToTable("CustomerImport");
        }
    }
}
