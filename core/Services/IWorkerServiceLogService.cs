using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;

namespace core.Services
{
    public interface IWorkerServiceLogService
    {
        Task<IEnumerable<WorkerServiceLog>> GetAllWorkerServiceLog();
        Task<WorkerServiceLog> GetWorkerServiceLogById(int id);
        Task<WorkerServiceLog> GetWorkerServiceLogByStartDateTime(string start_time);
        Task<WorkerServiceLog> CreateWorkerServiceLog(WorkerServiceLog newWorkerServicelog);
        Task UpdateWorkerServiceLog(WorkerServiceLog WorkerServicelogToBeUpdated, WorkerServiceLog WorkerServicelog);
        Task DeleteWorkerServiceLog(WorkerServiceLog WorkerServicelog);
    }
}
