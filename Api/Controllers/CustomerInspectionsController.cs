
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

    public class CustomerInspectionsController : ControllerBase
    {
        private readonly ICustomerInspectionService _customerInspectionService;
        private readonly IMapper _mapper;
        public CustomerInspectionsController(ICustomerInspectionService musicService, IMapper mapper)
        {
            this._mapper = mapper;
            this._customerInspectionService = musicService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CustomerInspection>>> GetAllCustomerInspections()
        {
            var customerInspections = await _customerInspectionService.GetAllWithCustomer();
            
            var customerInspectionsResources = _mapper.Map<IEnumerable<CustomerInspection>, IEnumerable<CustomerInspectionResource>>(customerInspections);

            return Ok(customerInspectionsResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInspectionResource>> GetCustomerInspectionById(int id)
        {
            var cInspection = await _customerInspectionService.GetCustomerInspectionById(id);
            var customerInspectionResource = _mapper.Map<CustomerInspection, CustomerInspectionResource>(cInspection);

            return Ok(customerInspectionResource);
        }

        [EnableCors]
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInspection>>> GetCustomerInspectionByCustomerId(int id)
        {
            var cInspection = await _customerInspectionService.GetCustomerInspectionByCustomerId(id);
            var customerInspectionResource = _mapper.Map<IEnumerable<CustomerInspection>, IEnumerable<CustomerInspectionResource>>(cInspection);

            return Ok(customerInspectionResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<CustomerInspectionResource>> CreateCustomerInspection([FromBody] CustomerInspectionResource saveCustomerInspectionResource)
        {
            var validator = new SaveCustomerInspectionResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerInspectionResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToCreate = _mapper.Map<CustomerInspectionResource, CustomerInspection>(saveCustomerInspectionResource);

            var newMusic = await _customerInspectionService.CreateCustomerInspection(musicToCreate);

            var music = await _customerInspectionService.GetCustomerInspectionById(newMusic.Id);

            var musicResource = _mapper.Map<CustomerInspection, CustomerInspectionResource>(music);

            return Ok(musicResource);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerInspectionResource>> UpdateCustomerInspection(int id, [FromBody] CustomerInspectionResource saveMusicResource)
        {
            var validator = new SaveCustomerInspectionResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToBeUpdate = await _customerInspectionService.GetCustomerInspectionById(id);

            if (musicToBeUpdate == null)
                return NotFound();





            var music = _mapper.Map<CustomerInspectionResource, CustomerInspection>(saveMusicResource);

            await _customerInspectionService.UpdateCustomerInspection(musicToBeUpdate, music);

            var updatedCustomerInspection = await _customerInspectionService.GetCustomerInspectionById(id);
            var updatedCustomerInspectionResource = _mapper.Map<CustomerInspection, CustomerInspectionResource>(updatedCustomerInspection);

            return Ok(updatedCustomerInspectionResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInspection(int id)
        {
            if (id == 0)
                return BadRequest();

            var customerInspection = await _customerInspectionService.GetCustomerInspectionById(id);

            if (customerInspection == null)
                return NotFound();

            await _customerInspectionService.DeleteCustomerInspection(customerInspection);


            return NoContent();
        }
       
    }
}
