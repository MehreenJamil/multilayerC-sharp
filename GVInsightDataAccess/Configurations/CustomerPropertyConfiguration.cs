using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVInsightDataAccess.Configurations
{
    class CustomerPropertyConfiguration : IEntityTypeConfiguration<CustomerProperty>
    {
        public void Configure(EntityTypeBuilder<CustomerProperty> builder)
        {
            builder
                 .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();



            builder
                .HasOne(m => m.Customer)
                .WithMany(a => a.CustomerProperties)
                .HasForeignKey(m => m.CustomerId);

            builder
                .ToTable("CustomerProperty");
        }
    }
}
