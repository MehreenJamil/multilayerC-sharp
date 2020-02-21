using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.Models;

namespace core.Services
{
    public interface ICustomerImportService
    {
        Task<IEnumerable<CustomerImport>> GetAllWithCustomer();
        Task<CustomerImport> GetCustomerImportById(int id);
        Task<IEnumerable<CustomerImport>> GetCustomerImportByCustomerId(int customerId);
        Task<CustomerImport> CreateCustomerImport(CustomerImport newCustomerImport);
        Task UpdateCustomerImport(CustomerImport customerImportToBeUpdated, CustomerImport customerImport);
        Task DeleteCustomerImport(CustomerImport customerImport);
        
    }
}
