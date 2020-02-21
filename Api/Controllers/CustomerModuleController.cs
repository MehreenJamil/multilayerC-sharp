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
    public class CustomerModuleController : ControllerBase
    {
        private readonly ICustomerModuleService _customerModuleService;
        private readonly IMapper _mapper;

        public CustomerModuleController(ICustomerModuleService customerModuleService, IMapper mapper)
        {
            this._mapper = mapper;
            this._customerModuleService = customerModuleService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CustomerModuleResource>>> GetAllCustomerModules()
        {
            var CustomerModules = await _customerModuleService.GetAllCustomerModule();
            var CustomerModulesResources = _mapper.Map<IEnumerable<CustomerModule>, IEnumerable<CustomerModuleResource>>(CustomerModules);

            return Ok(CustomerModulesResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModuleResource>> GetCustomerModuleById(int id)
        {
            var cimport = await _customerModuleService.GetCustomerModuleById(id);
            var CustomerModuleResource = _mapper.Map<CustomerModule, CustomerModuleResource>(cimport);

            return Ok(CustomerModuleResource);
        }

        [EnableCors]
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModuleResource>>> GetCustomerModuleByCustomerId(int id)
        {
            var cimport = await _customerModuleService.GetCustomerModuleByCustomerId(id);
            var CustomerModuleResource = _mapper.Map<IEnumerable<CustomerModule>, IEnumerable<CustomerModuleResource>>(cimport);

            return Ok(CustomerModuleResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<CustomerModuleResource>> CreateCustomerModule([FromBody] CustomerModuleResource saveCustomerModuleResource)
        {
            var validator = new SaveCustomerModuleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerModuleResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToCreate = _mapper.Map<CustomerModuleResource, CustomerModule>(saveCustomerModuleResource);

            var newMusic = await _customerModuleService.CreateCustomerModule(musicToCreate);

            var music = await _customerModuleService.GetCustomerModuleById(newMusic.Id);

            var musicResource = _mapper.Map<CustomerModule, CustomerModuleResource>(music);

            return Ok(musicResource);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerModuleResource>> UpdateCustomerModule(int id, [FromBody] CustomerModuleResource saveMusicResource)
        {
            var validator = new SaveCustomerModuleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToBeUpdate = await _customerModuleService.GetCustomerModuleById(id);

            if (musicToBeUpdate == null)
                return NotFound();

            var music = _mapper.Map<CustomerModuleResource, CustomerModule>(saveMusicResource);

            await _customerModuleService.UpdateCustomerModule(musicToBeUpdate, music);

            var updatedCustomerModule = await _customerModuleService.GetCustomerModuleById(id);
            var updatedCustomerModuleResource = _mapper.Map<CustomerModule, CustomerModuleResource>(updatedCustomerModule);

            return Ok(updatedCustomerModuleResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerModule(int id)
        {
            if (id == 0)
                return BadRequest();

            var CustomerModule = await _customerModuleService.GetCustomerModuleById(id);

            if (CustomerModule == null)
                return NotFound();

            await _customerModuleService.DeleteCustomerModule(CustomerModule);


            return NoContent();
        }



    }
}
