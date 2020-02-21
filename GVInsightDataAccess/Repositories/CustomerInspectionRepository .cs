using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
    public class CustomerInspectionRepository : Repository<CustomerInspection>, ICustomerInspectionRepository
    {
        public CustomerInspectionRepository(MyCustomerDbContext context)
           : base(context)
        { }
        public async Task<IEnumerable<CustomerInspection>> GetAllWithCustomerAsync()
        {
            
            return await MyCustomerDbContext.CustomerInspections
                .Include(m => m.Customer)
                .ToListAsync();
        }

        public async Task<CustomerInspection> GetWithCustomerByIdAsync(int id)
        {
            return await MyCustomerDbContext.CustomerInspections
                .Include(m => m.Customer)
                .SingleOrDefaultAsync(m => m.Id == id); ;
        }

        public async Task<IEnumerable<CustomerInspection>> GetAllWithCustomerByCustomerIdAsync(int customerId)
        {
            return await MyCustomerDbContext.CustomerInspections
                .Include(m => m.Customer)
                .Where(m => m.CustomerId == customerId)
                .ToListAsync();
        }

        private MyCustomerDbContext MyCustomerDbContext
        {
            get { return Context as MyCustomerDbContext; }
        }
    }
}
