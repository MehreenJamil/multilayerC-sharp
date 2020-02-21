using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVInsightDataAccess.Configurations
{
    public class ActiveUserConfiguration : IEntityTypeConfiguration<ActiveUser>
    {
        public void Configure(EntityTypeBuilder<ActiveUser> builder)
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
              .Property(m => m.UserActiveDate)
              .IsRequired();

          

            builder
                .ToTable("ActiveCustomer");
        }
    }
}
