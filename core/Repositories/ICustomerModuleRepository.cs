using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface ICustomerModuleRepository : IRepository<CustomerModule>
    {
        Task<IEnumerable<CustomerModule>> GetAllCustomerModulesWithAsync();
        Task<CustomerModule> GetCustomerModuleByIdAsync(int id);
        Task<IEnumerable<CustomerModule>> GetAllCustomerModulesWithCustomerId(int id);
    }
}
