using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.Models;


namespace core.Services
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllModule();
        Task<Module> GetModuleById(int id);
        Task<Module> CreateModule(Module newModule);
        Task UpdateModule(Module moduleToBeUpdated, Module module);
        Task DeleteModule(Module module);
    }
}
