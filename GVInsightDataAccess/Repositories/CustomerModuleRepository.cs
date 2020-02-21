using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
   public class CustomerModuleRepository : Repository<CustomerModule>, ICustomerModuleRepository
    {
        public CustomerModuleRepository(MyCustomerDbContext context)
          : base(context)
        { }

        public async Task<IEnumerable<CustomerModule>> GetAllCustomerModulesWithAsync()
        {
            return await MyCustomerDbContext.CustomerModules
                 .Include(m => m.Customer)
                 .ToListAsync();
        }

        public async Task<IEnumerable<CustomerModule>> GetAllCustomerModulesWithCustomerId(int customerId)
        {
            return await MyCustomerDbContext.CustomerModules
               .Include(m => m.Customer)
               .Include(m => m.Module)
               .Where(m => m.CustomerId == customerId)
               .ToListAsync();

        }

     

        public async Task<CustomerModule> GetCustomerModuleByIdAsync(int id)
        {
            return await MyCustomerDbContext.CustomerModules
                .Include(m => m.Customer)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        private MyCustomerDbContext MyCustomerDbContext
        {
            get { return Context as MyCustomerDbContext; }
        }
    }
}
