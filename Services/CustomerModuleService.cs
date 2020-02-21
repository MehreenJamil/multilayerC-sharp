using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class CustomerModuleService : ICustomerModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerModule> CreateCustomerModule(CustomerModule newCustomerModule)
        {
            await _unitOfWork.CustomerModules.AddAsync(newCustomerModule);
            await _unitOfWork.CommitAsync();
            return newCustomerModule;
        }

        public async Task DeleteCustomerModule(CustomerModule CustomerModule)
        {
            _unitOfWork.CustomerModules.Remove(CustomerModule);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CustomerModule>> GetAllCustomerModule()
        {
            return await _unitOfWork.CustomerModules.GetAllAsync();
        }

        public async Task<IEnumerable<CustomerModule>> GetCustomerModuleByCustomerId(int customerId)
        {
            return await _unitOfWork.CustomerModules.GetAllCustomerModulesWithCustomerId(customerId);
        }

        public async Task<CustomerModule> GetCustomerModuleById(int id)
        {
            return await _unitOfWork.CustomerModules.GetCustomerModuleByIdAsync(id);
        }

        public async Task UpdateCustomerModule(CustomerModule CustomerModuleToBeUpdated, CustomerModule CustomerModule)
        {
            CustomerModuleToBeUpdated.Title = CustomerModule.Title;
            CustomerModuleToBeUpdated.CustomerId = CustomerModule.CustomerId;
            CustomerModuleToBeUpdated.ModuleId = CustomerModule.ModuleId;

            await _unitOfWork.CommitAsync();
        }
    }
}
