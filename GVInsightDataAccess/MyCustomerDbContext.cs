using Microsoft.EntityFrameworkCore;
using core.Models;
using GVInsightDataAccess.Configurations;

namespace GVInsightDataAccess
{
    public class MyCustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerImport> CustomerImports { get; set; }
        public DbSet<WorkerServiceLog> WorkerServiceLogs { get; set; }
        public DbSet<CustomerInspection> CustomerInspections { get; set; }
        public DbSet<ActiveUser> ActiveUsers { get; set; }
        public DbSet<CustomerModule> CustomerModules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<CustomerProperty> CustomerProperties { get; set; }


        public MyCustomerDbContext(DbContextOptions<MyCustomerDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerImportConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new WorkerServiceLogConfiguration());
            builder.ApplyConfiguration(new CustomerInspectionConfiguration());
            builder.ApplyConfiguration(new ActiveUserConfiguration());
            builder.ApplyConfiguration(new ModuleConfiguration());
            builder.ApplyConfiguration(new CustomerModuleConfiguration());
            builder.ApplyConfiguration(new CustomerPropertyConfiguration());

        }
    }
}
