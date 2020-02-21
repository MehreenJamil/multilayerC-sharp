using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVInsightDataAccess.Configurations
{
    public class CustomerModuleConfiguration : IEntityTypeConfiguration<CustomerModule>
    {
       
            public void Configure(EntityTypeBuilder<CustomerModule> builder)
            {
                builder
                     .HasKey(m => m.Id);

                builder
                    .Property(m => m.Id)
                    .UseIdentityColumn();

            builder
             .HasOne(m => m.Customer)
             .WithMany(a => a.CustomerModules)
             .HasForeignKey(m => m.CustomerId);

            builder
                    .ToTable("CustomerModule");
            }
       
    }
}
