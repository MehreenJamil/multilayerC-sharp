using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface IModuleRepository : IRepository<Module>
    {
        Task<IEnumerable<Module>> GetAllModulesWithAsync();
        Task<Module> GetModuleByIdAsync(int id);
        
    }
}
