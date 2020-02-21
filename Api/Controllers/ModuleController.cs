using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core.Models;
using core.Services;
using AutoMapper;
using Api.Resources;
using Api.Validators;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        private readonly IMapper _mapper;
        public ModuleController(IModuleService _moduleService, IMapper mapper)
        {
            this._mapper = mapper;
            this._moduleService = _moduleService;
        }


        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ModuleResource>>> GetAllModules()
        {

            var Modules = await _moduleService.GetAllModule();
            var ModulesResources = _mapper.Map<IEnumerable<Module>, IEnumerable<ModuleResource>>(Modules);

            return Ok(ModulesResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleResource>> GetModuleById(int id)
        {
            var cimport = await _moduleService.GetModuleById(id);
            var ModuleResource = _mapper.Map<Module, ModuleResource>(cimport);

            return Ok(ModuleResource);
        }


        [HttpPost("")]
        public async Task<ActionResult<ModuleResource>> CreateModule([FromBody] ModuleResource saveModuleResource)
        {
            var validator = new SaveModuleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveModuleResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var ModuleToCreate = _mapper.Map<ModuleResource, Module>(saveModuleResource);

            var newModule = await _moduleService.CreateModule(ModuleToCreate);

            var Module = await _moduleService.GetModuleById(newModule.Id);

            var ModuleResource = _mapper.Map<Module, ModuleResource>(Module);

            return Ok(ModuleResource);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<ModuleResource>> UpdateModule(int id, [FromBody] ModuleResource saveModuleResource)
        {
            var validator = new SaveModuleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveModuleResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var ModuleToBeUpdate = await _moduleService.GetModuleById(id);

            if (ModuleToBeUpdate == null)
                return NotFound();



            var Module = _mapper.Map<ModuleResource, Module>(saveModuleResource);

            await _moduleService.UpdateModule(ModuleToBeUpdate, Module);

            var updatedModule = await _moduleService.GetModuleById(id);
            var updatedModuleResource = _mapper.Map<Module, ModuleResource>(updatedModule);

            return Ok(updatedModuleResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            if (id == 0)
                return BadRequest();

            var Module = await _moduleService.GetModuleById(id);

            if (Module == null)
                return NotFound();

            await _moduleService.DeleteModule(Module);


            return NoContent();
        }
    
}
}
