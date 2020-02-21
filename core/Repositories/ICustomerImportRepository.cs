using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface ICustomerImportRepository : IRepository<CustomerImport>
    {
        Task<IEnumerable<CustomerImport>> GetAllWithCustomerAsync();
        Task<CustomerImport> GetWithCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerImport>> GetAllWithCustomerByCustomerIdAsync(int customerId);
    }
}
