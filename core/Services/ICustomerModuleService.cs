using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.Models;

namespace core.Services
{
    public interface ICustomerModuleService
    {

        //Task<IEnumerable<CustomerModule>> GetAllWithCustomer();
        Task<CustomerModule> GetCustomerModuleById(int id);
        Task<IEnumerable<CustomerModule>> GetCustomerModuleByCustomerId(int customerId);
        Task<IEnumerable<CustomerModule>> GetAllCustomerModule();
        Task<CustomerModule> CreateCustomerModule(CustomerModule newCustomerModule);
        Task UpdateCustomerModule(CustomerModule CustomerModuleToBeUpdated, CustomerModule CustomerModule);
        Task DeleteCustomerModule(CustomerModule CustomerModule);
    }
}
