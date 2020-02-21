using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.Models;

namespace core.Services
{
    public interface ICustomerPropertyService
    {
        Task<IEnumerable<CustomerProperty>> GetAllWithCustomer();
        Task<CustomerProperty> GetCustomerPropertyById(int id);
        Task<IEnumerable<CustomerProperty>> GetCustomerPropertyByCustomerId(int customerId);
        Task<CustomerProperty> CreateCustomerProperty(CustomerProperty newCustomerProperty);
        Task UpdateCustomerProperty(CustomerProperty customerPropertyToBeUpdated, CustomerProperty customerProperty);
        Task DeleteCustomerProperty(CustomerProperty customerProperty);
    }
}
