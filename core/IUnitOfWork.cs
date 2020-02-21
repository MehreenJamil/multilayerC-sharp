using System;
using System.Collections.Generic;
using System.Text;
using core.Repositories;
using System.Threading.Tasks;

namespace core
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        ICustomerImportRepository CustomerImports { get; }
        IWorkerServiceLogRepository WorkerServiceLogs { get; }
        ICustomerInspectionRepository CustomerInspections { get; }
        IActiveUserRepository ActiveUsers { get; }
        IModuleRepository Modules { get; }
        ICustomerModuleRepository CustomerModules { get; }
        ICustomerPropertyRepository CustomerProperties { get; }

        Task<int> CommitAsync();
    }
}
