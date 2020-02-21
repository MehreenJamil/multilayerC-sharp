using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.Models;


namespace core.Services
{
    public interface ICustomerInspectionService
    {
        Task<IEnumerable<CustomerInspection>> GetAllWithCustomer();
        Task<CustomerInspection> GetCustomerInspectionById(int id);
        Task<IEnumerable<CustomerInspection>> GetCustomerInspectionByCustomerId(int customerId);
        Task<CustomerInspection> CreateCustomerInspection(CustomerInspection newCustomerInspection);
        Task UpdateCustomerInspection(CustomerInspection customerInspectionToBeUpdated, CustomerInspection customerInspection);
        Task DeleteCustomerInspection(CustomerInspection customerInspection);
    }
}
