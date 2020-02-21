using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class CustomerInspectionService : ICustomerInspectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerInspectionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CustomerInspection> CreateCustomerInspection(CustomerInspection newCustomerInspection)
        {
            await _unitOfWork.CustomerInspections.AddAsync(newCustomerInspection);
            await _unitOfWork.CommitAsync();
            return newCustomerInspection;
        }

        public async Task DeleteCustomerInspection(CustomerInspection customerInspection)
        {
            _unitOfWork.CustomerInspections.Remove(customerInspection);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CustomerInspection>> GetAllWithCustomer()
        {
            return await _unitOfWork.CustomerInspections.GetAllWithCustomerAsync();
        }

        public async Task<IEnumerable<CustomerInspection>> GetCustomerInspectionByCustomerId(int customerId)
        {
            return await _unitOfWork.CustomerInspections.GetAllWithCustomerByCustomerIdAsync(customerId);
        }

        public async Task<CustomerInspection> GetCustomerInspectionById(int id)
        {
            return await _unitOfWork.CustomerInspections.GetWithCustomerByIdAsync(id);
        }

        public async Task UpdateCustomerInspection(CustomerInspection customerInspectionToBeUpdated, CustomerInspection customerInspection)
        {

            //if (customerInspection.LastInspection == DateTime.MinValue)
            //{
            //    throw new Exception("LastInspection must be valid DateTme");
            //}

          
            customerInspectionToBeUpdated.CustomerId = customerInspection.CustomerId;




            customerInspectionToBeUpdated.Count = customerInspection.Count;
            customerInspectionToBeUpdated.CompletedDatetime = customerInspection.CompletedDatetime;

            

            await _unitOfWork.CommitAsync();
        }

    }
}
