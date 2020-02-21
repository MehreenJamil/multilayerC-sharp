using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(MyCustomerDbContext context)
          : base(context)
        { }
        private MyCustomerDbContext MyCustomerDbContext
        {
            get { return Context as MyCustomerDbContext; }
        }

        public async Task<IEnumerable<Module>> GetAllModulesWithAsync()
        {
            return await MyCustomerDbContext.Modules.ToListAsync();
        }

        public async Task<Module> GetModuleByIdAsync(int id)
        {
            return await MyCustomerDbContext.Modules
               .SingleOrDefaultAsync(m => m.Id == id); 
        }
    }
}
