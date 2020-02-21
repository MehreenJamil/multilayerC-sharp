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
    public class ActiveUserController : ControllerBase
    {
      
        private readonly IActiveUserService _activeUserService;
        private readonly IMapper _mapper;
        public ActiveUserController(IActiveUserService activeUserService, IMapper mapper)
        {
            this._mapper = mapper;
            this._activeUserService = activeUserService;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ActiveUserResource>>> GetAllActiveUsers()
        {
            
            var ActiveUsers = await _activeUserService.GetAllActiveUser();
            var ActiveUsersResources = _mapper.Map<IEnumerable<ActiveUser>, IEnumerable<ActiveUserResource>>(ActiveUsers);

            return Ok(ActiveUsersResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActiveUserResource>> GetActiveUserById(int id)
        {
            var cimport = await _activeUserService.GetActiveUserById(id);
            var ActiveUserResource = _mapper.Map<ActiveUser, ActiveUserResource>(cimport);

            return Ok(ActiveUserResource);
        }

       
        [HttpPost("")]
        public async Task<ActionResult<ActiveUserResource>> CreateActiveUser([FromBody] ActiveUserResource saveActiveUserResource)
        {
            var validator = new SaveActiveUserResourceValidator();
            var validationResult = await validator.ValidateAsync(saveActiveUserResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var activeUserToCreate = _mapper.Map<ActiveUserResource, ActiveUser>(saveActiveUserResource);

            var newactiveUser = await _activeUserService.CreateActiveUser(activeUserToCreate);

            var activeUser = await _activeUserService.GetActiveUserById(newactiveUser.Id);

            var activeUserResource = _mapper.Map<ActiveUser, ActiveUserResource>(activeUser);

            return Ok(activeUserResource);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<ActiveUserResource>> UpdateActiveUser(int id, [FromBody] ActiveUserResource saveactiveUserResource)
        {
            var validator = new SaveActiveUserResourceValidator();
            var validationResult = await validator.ValidateAsync(saveactiveUserResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var activeUserToBeUpdate = await _activeUserService.GetActiveUserById(id);

            if (activeUserToBeUpdate == null)
                return NotFound();



            var activeUser = _mapper.Map<ActiveUserResource, ActiveUser>(saveactiveUserResource);

            await _activeUserService.UpdateActiveUser(activeUserToBeUpdate, activeUser);

            var updatedActiveUser = await _activeUserService.GetActiveUserById(id);
            var updatedActiveUserResource = _mapper.Map<ActiveUser, ActiveUserResource>(updatedActiveUser);

            return Ok(updatedActiveUserResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiveUser(int id)
        {
            if (id == 0)
                return BadRequest();

            var ActiveUser = await _activeUserService.GetActiveUserById(id);

            if (ActiveUser == null)
                return NotFound();

            await _activeUserService.DeleteActiveUser(ActiveUser);


            return NoContent();
        }
    }
}
