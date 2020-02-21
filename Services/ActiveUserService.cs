using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class ActiveUserService : IActiveUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActiveUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActiveUser> CreateActiveUser(ActiveUser newActiveUser)
        {
            await _unitOfWork.ActiveUsers.AddAsync(newActiveUser);
            await _unitOfWork.CommitAsync();
            return newActiveUser;
        }

        public async Task DeleteActiveUser(ActiveUser ActiveUser)
        {
            _unitOfWork.ActiveUsers.Remove(ActiveUser);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ActiveUser> GetActiveUserById(int id)
        {
            return await _unitOfWork.ActiveUsers.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ActiveUser>> GetAllActiveUser()
        {
            return await _unitOfWork.ActiveUsers.GetAllActiveUserAsync();
        }

        public async Task UpdateActiveUser(ActiveUser ActiveUserToBeUpdated, ActiveUser ActiveUser)
        {
            ActiveUserToBeUpdated.Count = ActiveUser.Count;
            ActiveUserToBeUpdated.UserActiveDate = ActiveUser.UserActiveDate;

            await _unitOfWork.CommitAsync();
        }
    }
}
