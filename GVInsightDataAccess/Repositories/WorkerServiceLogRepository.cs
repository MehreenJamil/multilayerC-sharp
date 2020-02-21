using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Repositories;

namespace GVInsightDataAccess.Repositories
{
    public class WorkerServiceLogRepository : Repository<WorkerServiceLog>, IWorkerServiceLogRepository
    {
        public WorkerServiceLogRepository(MyCustomerDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<WorkerServiceLog>> GetAllWorkerServiceLogsWithAsync()
        {
            return await MyCustomerDbContext.WorkerServiceLogs.ToListAsync();

        }

      
        public async Task<WorkerServiceLog> GetWorkerServiceLogWithByIdAsync(int id)
        {

            return await MyCustomerDbContext.WorkerServiceLogs.SingleOrDefaultAsync(m => m.Id == id);

        }
        public async Task<WorkerServiceLog> GetWorkerServiceLogWithByStartDateTimeAsync(string start_time)
        {
  
            return await MyCustomerDbContext.WorkerServiceLogs.SingleOrDefaultAsync(m => m.Start_time.Equals(start_time));

        }

        private MyCustomerDbContext MyCustomerDbContext
        {
            get { return Context as MyCustomerDbContext; }
        }
    }
}
