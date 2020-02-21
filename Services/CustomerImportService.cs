using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class CustomerImportService : ICustomerImportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerImportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CustomerImport> CreateCustomerImport(CustomerImport newCustomerImport)
        {
            await _unitOfWork.CustomerImports.AddAsync(newCustomerImport);
            await _unitOfWork.CommitAsync();
            return newCustomerImport;
        }

        public async Task DeleteCustomerImport(CustomerImport customerImport)
        {
            _unitOfWork.CustomerImports.Remove(customerImport);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CustomerImport>> GetAllWithCustomer()
        {
            return await _unitOfWork.CustomerImports.GetAllWithCustomerAsync();
        }

        public async Task<IEnumerable<CustomerImport>> GetCustomerImportByCustomerId(int customerId)
        {
            return await _unitOfWork.CustomerImports.GetAllWithCustomerByCustomerIdAsync(customerId);
        }

        public async Task<CustomerImport> GetCustomerImportById(int id)
        {
            return await _unitOfWork.CustomerImports.GetWithCustomerByIdAsync(id);
        }

        public async Task UpdateCustomerImport(CustomerImport customerImportToBeUpdated, CustomerImport customerImport)
        {

            if (customerImport.LastImport == DateTime.MinValue)
            {
                throw new Exception("LastImport must be valid DateTme");
            }

            if (!string.IsNullOrEmpty(customerImport.Name))
                customerImportToBeUpdated.Name = customerImport.Name;
            customerImportToBeUpdated.CustomerId = customerImport.CustomerId;


            

            customerImportToBeUpdated.LastImport = customerImport.LastImport;
            customerImportToBeUpdated.NextImport = customerImport.NextImport;

            customerImportToBeUpdated.Removed = customerImport.Removed;
            
            customerImportToBeUpdated.ExternalImportId = customerImport.ExternalImportId;



            await _unitOfWork.CommitAsync();
        }

        //public async Task<JsonResult> getCustomerImportdashboardInfo(int customerId) { 
        
        //}
    }
}
