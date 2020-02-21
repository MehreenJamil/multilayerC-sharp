using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface ICustomerInspectionRepository : IRepository<CustomerInspection>
    {
        Task<IEnumerable<CustomerInspection>> GetAllWithCustomerAsync();
        Task<CustomerInspection> GetWithCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerInspection>> GetAllWithCustomerByCustomerIdAsync(int customerId);
    }
}
