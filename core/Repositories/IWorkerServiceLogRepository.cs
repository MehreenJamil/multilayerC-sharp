using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Repositories
{
    public interface IWorkerServiceLogRepository : IRepository<WorkerServiceLog>
    {
        Task<IEnumerable<WorkerServiceLog>> GetAllWorkerServiceLogsWithAsync();
        Task<WorkerServiceLog> GetWorkerServiceLogWithByIdAsync(int id);
        Task<WorkerServiceLog> GetWorkerServiceLogWithByStartDateTimeAsync(string start_time);

    }
}