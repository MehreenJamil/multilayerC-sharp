using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersWithAsync();
        Task<Customer> GetCustomerWithByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersWithCustomerImports();

    }
}
