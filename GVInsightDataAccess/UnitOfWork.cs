using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core;
using core.Repositories;
using GVInsightDataAccess.Repositories;

namespace GVInsightDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyCustomerDbContext _context;
        private CustomerRepository _customerRepository;
        private CustomerImportRepository _customerImportRepository;
        private WorkerServiceLogRepository _workerServiceLogRepository;
        private CustomerInspectionRepository _customerInspectionRepository;
        private ActiveUserRepository _activeUserRepository;
        private ModuleRepository _moduleRepository;
        private CustomerModuleRepository _customerModuleRepository;
        private CustomerPropertyRepository _customerPropertyRepository;


        public UnitOfWork(MyCustomerDbContext context)
        {
            this._context = context;
        }

        public ICustomerRepository Customers => _customerRepository = _customerRepository ?? new CustomerRepository(_context);
        public ICustomerImportRepository CustomerImports => _customerImportRepository = _customerImportRepository ?? new CustomerImportRepository(_context);
        public IWorkerServiceLogRepository WorkerServiceLogs => _workerServiceLogRepository = _workerServiceLogRepository ?? new WorkerServiceLogRepository(_context);

        public ICustomerInspectionRepository CustomerInspections => _customerInspectionRepository = _customerInspectionRepository ?? new CustomerInspectionRepository(_context);
        public IActiveUserRepository ActiveUsers => _activeUserRepository = _activeUserRepository ?? new ActiveUserRepository(_context);

        public IModuleRepository Modules => _moduleRepository = _moduleRepository ?? new ModuleRepository(_context);

        public ICustomerModuleRepository CustomerModules => _customerModuleRepository = _customerModuleRepository ?? new CustomerModuleRepository(_context);
        public ICustomerPropertyRepository CustomerProperties => _customerPropertyRepository = _customerPropertyRepository ?? new CustomerPropertyRepository(_context);



        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
           
                
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
