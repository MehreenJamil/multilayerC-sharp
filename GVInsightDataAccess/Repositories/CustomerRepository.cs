using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
   public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyCustomerDbContext context)
            : base(context)
        { }
        
        public async Task<IEnumerable<Customer>> GetAllCustomersWithAsync()
        {
            return await MyCustomerDbContext.Customers.ToListAsync();
           
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersWithCustomerImports()
        {
            return await MyCustomerDbContext.Customers.Include(x => x.CustomerImports).ToListAsync();
        }

        public async Task<Customer> GetCustomerWithByIdAsync(int id)
        {
            
            return await MyCustomerDbContext.Customers.SingleOrDefaultAsync(m => m.Id == id);

        }

        private MyCustomerDbContext MyCustomerDbContext
        {
            get { return Context as MyCustomerDbContext; }
        }
    }
}
