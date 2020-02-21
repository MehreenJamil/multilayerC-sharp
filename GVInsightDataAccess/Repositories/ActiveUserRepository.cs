using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
   public class ActiveUserRepository : Repository<ActiveUser>, IActiveUserRepository
    {
    public ActiveUserRepository(MyCustomerDbContext context)
         : base(context)
    { }
    public async Task<IEnumerable<ActiveUser>> GetAllActiveUserAsync()
    {
            return await MyCustomerDbContext.ActiveUsers.ToListAsync();
           
    }

    private MyCustomerDbContext MyCustomerDbContext
    {
        get { return Context as MyCustomerDbContext; }
    }
}
}
