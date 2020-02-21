using Microsoft.EntityFrameworkCore;
using core.Models;
using GVInsightDataAccess.Configurations;

namespace GVInsightDataAccess
{
    public class MyCustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        

        public MyCustomerDbContext(DbContextOptions<MyCustomerDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());

        }
    }
}
