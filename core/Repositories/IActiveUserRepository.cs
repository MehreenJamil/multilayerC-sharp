using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface IActiveUserRepository : IRepository<ActiveUser>
    {
        Task<IEnumerable<ActiveUser>> GetAllActiveUserAsync();
        
        
    }
}
