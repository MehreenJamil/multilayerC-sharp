using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class CustomerPropertiesService : ICustomerPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerPropertiesService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CustomerProperty> CreateCustomerProperty(CustomerProperty newCustomerProperty)
        {
            await _unitOfWork.CustomerProperties.AddAsync(newCustomerProperty);
            await _unitOfWork.CommitAsync();
            return newCustomerProperty;
        }

        public async Task DeleteCustomerProperty(CustomerProperty customerProperty)
        {
            _unitOfWork.CustomerProperties.Remove(customerProperty);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CustomerProperty>> GetAllWithCustomer()
        {
            return await _unitOfWork.CustomerProperties.GetAllWithCustomerAsync();
        }

        public async Task<IEnumerable<CustomerProperty>> GetCustomerPropertyByCustomerId(int customerId)
        {
            return await _unitOfWork.CustomerProperties.GetAllWithCustomerByCustomerIdAsync(customerId);
        }

        public async Task<CustomerProperty> GetCustomerPropertyById(int id)
        {
            return await _unitOfWork.CustomerProperties.GetWithCustomerByIdAsync(id);
        }

        public async Task UpdateCustomerProperty(CustomerProperty customerPropertyToBeUpdated, CustomerProperty customerProperty)
        {
            customerPropertyToBeUpdated.CustomerId = customerProperty.CustomerId;
            customerPropertyToBeUpdated.Name = customerProperty.Name;
            customerPropertyToBeUpdated.Address = customerProperty.Address;
            customerPropertyToBeUpdated.Latitude = customerProperty.Latitude;
            customerPropertyToBeUpdated.Longitude = customerProperty.Longitude;
            customerPropertyToBeUpdated.Size = customerProperty.Size;
            customerPropertyToBeUpdated.Year = customerProperty.Year;
            customerPropertyToBeUpdated.ExternalId = customerProperty.ExternalId;
            customerPropertyToBeUpdated.Enabled = customerProperty.Enabled;


            await _unitOfWork.CommitAsync();
        }

    }
}
