using System.Collections.Generic;
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
    public class CustomerPropertiesController : ControllerBase
    {
        private readonly ICustomerPropertyService _customerPropertyService;
        private readonly IMapper _mapper;
        public CustomerPropertiesController(ICustomerPropertyService musicService, IMapper mapper)
        {
            this._mapper = mapper;
            this._customerPropertyService = musicService;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CustomerProperty>>> GetAllCustomerPropertys()
        {
            var customerPropertys = await _customerPropertyService.GetAllWithCustomer();

            var customerPropertysResources = _mapper.Map<IEnumerable<CustomerProperty>, IEnumerable<CustomerPropertyResource>>(customerPropertys);

            return Ok(customerPropertysResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPropertyResource>> GetCustomerPropertyById(int id)
        {
            var cProperty = await _customerPropertyService.GetCustomerPropertyById(id);
            var customerPropertyResource = _mapper.Map<CustomerProperty, CustomerPropertyResource>(cProperty);

            return Ok(customerPropertyResource);
        }

        [EnableCors]
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerProperty>>> GetCustomerPropertyByCustomerId(int id)
        {
            var cProperty = await _customerPropertyService.GetCustomerPropertyByCustomerId(id);
            var customerPropertyResource = _mapper.Map<IEnumerable<CustomerProperty>, IEnumerable<CustomerPropertyResource>>(cProperty);

            return Ok(customerPropertyResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<CustomerPropertyResource>> CreateCustomerProperty([FromBody] CustomerPropertyResource saveCustomerPropertyResource)
        {
            var validator = new SaveCustomerPropertyResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerPropertyResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToCreate = _mapper.Map<CustomerPropertyResource, CustomerProperty>(saveCustomerPropertyResource);

            var newMusic = await _customerPropertyService.CreateCustomerProperty(musicToCreate);

            var music = await _customerPropertyService.GetCustomerPropertyById(newMusic.Id);

            var musicResource = _mapper.Map<CustomerProperty, CustomerPropertyResource>(music);

            return Ok(musicResource);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerPropertyResource>> UpdateCustomerProperty(int id, [FromBody] CustomerPropertyResource saveMusicResource)
        {
            var validator = new SaveCustomerPropertyResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToBeUpdate = await _customerPropertyService.GetCustomerPropertyById(id);

            if (musicToBeUpdate == null)
                return NotFound();





            var music = _mapper.Map<CustomerPropertyResource, CustomerProperty>(saveMusicResource);

            await _customerPropertyService.UpdateCustomerProperty(musicToBeUpdate, music);

            var updatedCustomerProperty = await _customerPropertyService.GetCustomerPropertyById(id);
            var updatedCustomerPropertyResource = _mapper.Map<CustomerProperty, CustomerPropertyResource>(updatedCustomerProperty);

            return Ok(updatedCustomerPropertyResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerProperty(int id)
        {
            if (id == 0)
                return BadRequest();

            var customerProperty = await _customerPropertyService.GetCustomerPropertyById(id);

            if (customerProperty == null)
                return NotFound();

            await _customerPropertyService.DeleteCustomerProperty(customerProperty);


            return NoContent();
        }
    }
}
