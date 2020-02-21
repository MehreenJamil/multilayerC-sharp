using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;
namespace core.Services
{
    public interface ICustomerService
    {
        
        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<IEnumerable<Customer>> GetAllCustomersWithImports();
        
        Task<Customer> GetCustomerById(int id);
        //Task<IEnumerable<Customer>> GetCustomerImportByCustomertId(int customerId);

        Task<Customer> CreateCustomer(Customer newCustomer);
        Task UpdateCustomer(Customer CustomerToBeUpdated, Customer customer);
        Task DeleteCustomer(Customer Customer);




    }
}

