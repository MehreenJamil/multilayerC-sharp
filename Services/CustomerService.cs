using System.Collections.Generic;
using System.Threading.Tasks;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Customer> CreateCustomer(Customer newCustomer)
        {
            await _unitOfWork.Customers.AddAsync(newCustomer);
            await _unitOfWork.CommitAsync();
            return newCustomer;
        }

        public async Task DeleteCustomer(Customer Customer)
        {
            _unitOfWork.Customers.Remove(Customer);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersWithImports()
        {
            return await _unitOfWork.Customers.GetAllCustomersWithCustomerImports();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }


        public async Task UpdateCustomer(Customer CustomerToBeUpdated, Customer customer)
        {
           
            CustomerToBeUpdated.Name = customer.Name;
            CustomerToBeUpdated.HostUrl = customer.HostUrl;
            CustomerToBeUpdated.ApiUserName = customer.ApiUserName;
            CustomerToBeUpdated.ApiPassword = customer.ApiPassword;
            CustomerToBeUpdated.Removed = customer.Removed;
            CustomerToBeUpdated.CustomerColor = customer.CustomerColor;
            CustomerToBeUpdated.image = customer.image;



        await _unitOfWork.CommitAsync();
        }

        
    }
}
