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
   
    public class CustomerImportsController : ControllerBase
    {
        private readonly ICustomerImportService _customerImportService;
        private readonly IMapper _mapper;
        public CustomerImportsController(ICustomerImportService musicService, IMapper mapper)
        {
            this._mapper = mapper;
            this._customerImportService = musicService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CustomerImportResource>>> GetAllCustomerImports()
        {
            var customerimports = await _customerImportService.GetAllWithCustomer();
            var customerimportsResources = _mapper.Map<IEnumerable<CustomerImport>, IEnumerable<CustomerImportResource>>(customerimports);

            return Ok(customerimportsResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerImportResource>> GetCustomerImportById(int id)
        {
            var cimport = await _customerImportService.GetCustomerImportById(id);
            var customerImportResource = _mapper.Map<CustomerImport, CustomerImportResource>(cimport);

            return Ok(customerImportResource);
        }

        [EnableCors]
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerImportResource>>> GetCustomerImportByCustomerId(int id)
        {
            var cimport = await _customerImportService.GetCustomerImportByCustomerId(id);
            var customerImportResource = _mapper.Map<IEnumerable<CustomerImport>, IEnumerable<CustomerImportResource>>(cimport);

            return Ok(customerImportResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<CustomerImportResource>> CreateCustomerImport([FromBody] CustomerImportResource saveCustomerImportResource)
        {
            var validator = new SaveCustomerImportResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerImportResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToCreate = _mapper.Map<CustomerImportResource, CustomerImport>(saveCustomerImportResource);

            var newMusic = await _customerImportService.CreateCustomerImport(musicToCreate);

            var music = await _customerImportService.GetCustomerImportById(newMusic.Id);

            var musicResource = _mapper.Map<CustomerImport, CustomerImportResource>(music);

            return Ok(musicResource);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerImportResource>> UpdateCustomerImport(int id, [FromBody] CustomerImportResource saveMusicResource)
        {
            var validator = new SaveCustomerImportResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToBeUpdate = await _customerImportService.GetCustomerImportById(id);

            if (musicToBeUpdate == null)
                return NotFound();


            if (saveMusicResource.LastImport == null)
                saveMusicResource.LastImport = musicToBeUpdate.LastImport;
           

            if (saveMusicResource.NextImport == null)
                saveMusicResource.NextImport = musicToBeUpdate.NextImport;
            

            var music = _mapper.Map<CustomerImportResource, CustomerImport>(saveMusicResource);

            await _customerImportService.UpdateCustomerImport(musicToBeUpdate, music);

            var updatedCustomerImport = await _customerImportService.GetCustomerImportById(id);
            var updatedCustomerImportResource = _mapper.Map<CustomerImport, CustomerImportResource>(updatedCustomerImport);

            return Ok(updatedCustomerImportResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerImport(int id)
        {
            if (id == 0)
                return BadRequest();

            var customerImport = await _customerImportService.GetCustomerImportById(id);

            if (customerImport == null)
                return NotFound();

            await _customerImportService.DeleteCustomerImport(customerImport);
            

            return NoContent();
        }
        //[EnableCors]
        //[Route("[action]/{id}")]
        //[HttpGet]
        //public async Task<JsonResult> GetCustomerImportDashboardInfoByCustomerId(int id)
        //{
        //    var cimport = await _customerImportService.GetCustomerImportDashboardByCustomerId(id);
        //    var customerImportResource = _mapper.Map<IEnumerable<CustomerImport>, IEnumerable<CustomerImportResource>>(cimport);

        //    return Ok(customerImportResource);
        //}


    }
}
