using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.Models;

namespace GVInsightDataAccess.Configurations
{
    class WorkerServiceLogConfiguration : IEntityTypeConfiguration<WorkerServiceLog>
    {
        public void Configure(EntityTypeBuilder<WorkerServiceLog> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).UseIdentityColumn();
            builder.Property(m => m.Start_time)
               .IsRequired();
             

            builder.ToTable("WorkerServiceLog");
        }
    }
}