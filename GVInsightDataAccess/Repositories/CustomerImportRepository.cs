using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
    public class CustomerImportRepository : Repository<CustomerImport>, ICustomerImportRepository
    {
        public CustomerImportRepository(MyCustomerDbContext context)
           : base(context)
        { }
        public async Task<IEnumerable<CustomerImport>> GetAllWithCustomerAsync()
        {
            
            return await MyCustomerDbContext.CustomerImports
                .Include(m => m.Customer)
                .ToListAsync();
        }

        public async Task<CustomerImport> GetWithCustomerByIdAsync(int id)
        {
            return await MyCustomerDbContext.CustomerImports
                .Include(m => m.Customer)
                .SingleOrDefaultAsync(m => m.Id == id); ;
        }

        public async Task<IEnumerable<CustomerImport>> GetAllWithCustomerByCustomerIdAsync(int customerId)
        {
            return await MyCustomerDbContext.CustomerImports
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
