using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.Models;

namespace core.Services
{
    public interface IActiveUserService
    {
        Task<IEnumerable<ActiveUser>> GetAllActiveUser();
        Task<ActiveUser> GetActiveUserById(int id);
        Task<ActiveUser> CreateActiveUser(ActiveUser newActiveUser);
        Task UpdateActiveUser(ActiveUser ActiveUserToBeUpdated, ActiveUser ActiveUser);
        Task DeleteActiveUser(ActiveUser ActiveUser);
    }
}
