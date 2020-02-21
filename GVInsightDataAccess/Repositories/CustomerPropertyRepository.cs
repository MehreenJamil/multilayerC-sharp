using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
    public class CustomerPropertyRepository : Repository<CustomerProperty>, ICustomerPropertyRepository
    {
        public CustomerPropertyRepository(MyCustomerDbContext context)
         : base(context)
        { }
        public async Task<IEnumerable<CustomerProperty>> GetAllWithCustomerAsync()
        {

            return await MyCustomerDbContext.CustomerProperties
                .Include(m => m.Customer)
                .ToListAsync();
        }

        public async Task<CustomerProperty> GetWithCustomerByIdAsync(int id)
        {
            return await MyCustomerDbContext.CustomerProperties
                .Include(m => m.Customer)
                .SingleOrDefaultAsync(m => m.Id == id); ;
        }

        public async Task<IEnumerable<CustomerProperty>> GetAllWithCustomerByCustomerIdAsync(int customerId)
        {
            return await MyCustomerDbContext.CustomerProperties
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
