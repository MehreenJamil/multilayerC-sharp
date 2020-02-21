using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVInsightDataAccess.Configurations
{
    public class CustomerInspectionConfiguration : IEntityTypeConfiguration<CustomerInspection>
    {
        public void Configure(EntityTypeBuilder<CustomerInspection> builder)
        {
            builder
                 .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

           
            builder
               .Property(m => m.Count)
               .IsRequired();
            builder
              .Property(m => m.CompletedDatetime)
              .IsRequired();

            builder
                .HasOne(m => m.Customer)
                .WithMany(a => a.CustomerInspections)
                .HasForeignKey(m => m.CustomerId);

            builder
                .ToTable("CustomerInspection");
        }
    }
}
