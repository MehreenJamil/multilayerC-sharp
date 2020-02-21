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
    [ApiController]
    [Route("api/[controller]")]
   

    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
      
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            this._mapper = mapper;
            this._customerService = customerService;
        }

        [EnableCors]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomer()
        {
            var customers = await _customerService.GetAllCustomer();
            var musicResources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);

            return Ok(musicResources);
        }

        //[Route("[action]")]
        //[EnableCors]
        //[HttpGet("")]
        //public async Task<ActionResult<IEnumerable<object>>> GetStrangeCustomer()
        //{
        //    //var customers = await _customerService.GetAllCustomer();

        //    var customerImports = await _customerService.GetAllCustomersWithImports();

        //    //var myResult = customerImports.Select(x => new
        //    //{
        //    //    x.Name,
        //    //    x.Id,
        //    //    CountImports = x.CustomerImports.Count(),
        //    //    Imports = x.CustomerImports,

        //    //});


        //    var musicResources = _mapper.Map<IEnumerable<object>, IEnumerable<CustomerResource>>(customerImports);

        //    return Ok(musicResources);
        //}



        [EnableCors]
        [HttpGet("{id}/")]
        public async Task<ActionResult<CustomerResource>> GetCustomerById(int id)
        {
        
            var customer = await _customerService.GetCustomerById(id);
            var customerResource = _mapper.Map<Customer, CustomerResource>(customer);

            return Ok(customerResource);
        }
        [EnableCors]
        [HttpPost("")]
        public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CustomerResource saveCustomerResource)
        {

            

            var validator = new SaveCustomerResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok
            
            var customerToCreate = _mapper.Map<CustomerResource, Customer>(saveCustomerResource);
           

            var newCustomer = await _customerService.CreateCustomer(customerToCreate);

            var customer = await _customerService.GetCustomerById(newCustomer.Id);

            var customerResource = _mapper.Map<Customer, CustomerResource>(customer);

            return Ok(customerResource);
        }
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResource>> UpdateCustomer(int id, [FromBody] CustomerResource saveCustomerResource)
        {
            var validator = new SaveCustomerResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var customerToBeUpdate = await _customerService.GetCustomerById(id);

            if (customerToBeUpdate == null)
                return NotFound();

            var customer = _mapper.Map<CustomerResource, Customer>(saveCustomerResource);

            await _customerService.UpdateCustomer(customerToBeUpdate, customer);

            var updatedMusic = await _customerService.GetCustomerById(id);
            var updatedMusicResource = _mapper.Map<Customer, CustomerResource>(updatedMusic);

            return Ok(updatedMusicResource);
        }

        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id == 0)
                return BadRequest();

            var music = await _customerService.GetCustomerById(id);

            if (music == null)
                return NotFound();

            await _customerService.DeleteCustomer(music);

            return NoContent();
        }
        [EnableCors]
        [Route("[action]/{data}")]
        [HttpPut]
        public void saveimage(String data)
        {
            byte[] imgarr = Convert.FromBase64String(data);
            /* Add further code here*/
        }


    }
}
