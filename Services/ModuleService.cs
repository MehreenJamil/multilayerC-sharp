using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using core;
using core.Models;
using core.Services;

namespace Services
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Module> CreateModule(Module newModule)
        {
            await _unitOfWork.Modules.AddAsync(newModule);
            await _unitOfWork.CommitAsync();
            return newModule;
        }

        public async Task DeleteModule(Module module)
        {
            _unitOfWork.Modules.Remove(module);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Module>> GetAllModule()
        {
            return await _unitOfWork.Modules.GetAllModulesWithAsync();
        }

        public async Task<Module> GetModuleById(int id)
        {
            return await _unitOfWork.Modules.GetByIdAsync(id);
        }

        public async Task UpdateModule(Module moduleToBeUpdated, Module module)
        {
            moduleToBeUpdated.Name = module.Name;
            
            await _unitOfWork.CommitAsync();
        }
    }
}
