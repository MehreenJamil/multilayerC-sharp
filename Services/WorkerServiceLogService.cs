using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class WorkerServiceLogService : IWorkerServiceLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkerServiceLogService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<WorkerServiceLog> CreateWorkerServiceLog(WorkerServiceLog newWorkerServiceLog)
        {
            try
            {
              
                await _unitOfWork.WorkerServiceLogs.AddAsync(newWorkerServiceLog);
                
                await _unitOfWork.CommitAsync();
                return newWorkerServiceLog;
            }
            catch(Exception e)
            {
                return null;
            }
    
        }

        public async Task DeleteWorkerServiceLog(WorkerServiceLog workerServiceLog)
        {
            _unitOfWork.WorkerServiceLogs.Remove(workerServiceLog);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<WorkerServiceLog>> GetAllWorkerServiceLog()
        {
            return await _unitOfWork.WorkerServiceLogs.GetAllAsync();
        }

       

        public async Task<WorkerServiceLog> GetWorkerServiceLogById(int id)
        {
            return await _unitOfWork.WorkerServiceLogs.GetByIdAsync(id);
        }

        public async Task<WorkerServiceLog> GetWorkerServiceLogByStartDateTime(string start_time)
        {
            //await db.Foos.Where(x => x.UserId == userId).ToListAsync();
            return await _unitOfWork.WorkerServiceLogs.GetByStartTimeAsync(start_time);
        }

        public async Task UpdateWorkerServiceLog(WorkerServiceLog workerServiceLogToBeUpdated, WorkerServiceLog workerServiceLog)
        {

            workerServiceLogToBeUpdated.Start_time = workerServiceLog.Start_time;
            workerServiceLogToBeUpdated.End_time = workerServiceLog.End_time;
            workerServiceLogToBeUpdated.Exception = workerServiceLog.Exception;
            workerServiceLogToBeUpdated.Execution_time = workerServiceLog.Execution_time;


            await _unitOfWork.CommitAsync();
        }


    }
}

