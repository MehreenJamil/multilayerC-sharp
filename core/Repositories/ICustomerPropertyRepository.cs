using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface ICustomerPropertyRepository : IRepository<CustomerProperty>
    {
        Task<IEnumerable<CustomerProperty>> GetAllWithCustomerAsync();
        Task<CustomerProperty> GetWithCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerProperty>> GetAllWithCustomerByCustomerIdAsync(int customerId);
    }
}
