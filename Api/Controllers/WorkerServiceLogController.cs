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


    public class WorkerServiceLogController : ControllerBase
    {
        private readonly IWorkerServiceLogService _workerLogService;

        private readonly IMapper _mapper;

        public WorkerServiceLogController(IWorkerServiceLogService workerServiceLogService, IMapper mapper)
        {
            this._mapper = mapper;
            this._workerLogService = workerServiceLogService;
        }

        [EnableCors]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<WorkerServiceLog>>> GetAllWorkerServiceLog()
        {
            var workerServiceLogs = await _workerLogService.GetAllWorkerServiceLog();
            var workerLogResources = _mapper.Map<IEnumerable<WorkerServiceLog>, IEnumerable<WorkerServiceLogResource>>(workerServiceLogs);

            return Ok(workerLogResources.OrderByDescending(s => s.Start_time));
        }


        [EnableCors]
        [HttpGet("{id}/")]
        public async Task<ActionResult<WorkerServiceLogResource>> GetWorkerServiceLogById(int id)
        {

            var workerServiceLog = await _workerLogService.GetWorkerServiceLogById(id);
            var workerServiceLogResource = _mapper.Map<WorkerServiceLog, WorkerServiceLogResource>(workerServiceLog);

            return Ok(workerServiceLogResource);
        }
        [EnableCors]
        [HttpPost("")]
        public async Task<ActionResult<WorkerServiceLogResource>> CreateWorkerServiceLog([FromBody] WorkerServiceLogResource saveWorkerServiceLogResource)
        {



            var validator = new SaveWorkerServiceLogResourceValidator();
            var validationResult = await validator.ValidateAsync(saveWorkerServiceLogResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var workerServiceLogToCreate = _mapper.Map<WorkerServiceLogResource, WorkerServiceLog>(saveWorkerServiceLogResource);

            var newWorkerServiceLog = await _workerLogService.CreateWorkerServiceLog(workerServiceLogToCreate);

            var workerServiceLog = await _workerLogService.GetWorkerServiceLogById(newWorkerServiceLog.Id);

            var workerServiceLogResource = _mapper.Map<WorkerServiceLog, WorkerServiceLogResource>(workerServiceLog);

            return Ok(workerServiceLogResource);
        }
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkerServiceLogResource>> UpdateWorkerServiceLog(int id, [FromBody] WorkerServiceLogResource saveWorkerServiceLogResource)
        {
            var validator = new SaveWorkerServiceLogResourceValidator();
            var validationResult = await validator.ValidateAsync(saveWorkerServiceLogResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var WorkerServiceLogToBeUpdate = await _workerLogService.GetWorkerServiceLogById(id);

            if (WorkerServiceLogToBeUpdate == null)
                return NotFound();

            var workerServiceLog = _mapper.Map<WorkerServiceLogResource, WorkerServiceLog>(saveWorkerServiceLogResource);

            await _workerLogService.UpdateWorkerServiceLog(WorkerServiceLogToBeUpdate, workerServiceLog);

            var updatedWorkerLog = await _workerLogService.GetWorkerServiceLogById(id);
            var updatedupdatedWorkerLogResource = _mapper.Map<WorkerServiceLog, WorkerServiceLogResource>(updatedWorkerLog);

            return Ok(updatedupdatedWorkerLogResource);
        }

        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerServiceLog(int id)
        {
            if (id == 0)
                return BadRequest();

            var music = await _workerLogService.GetWorkerServiceLogById(id);

            if (music == null)
                return NotFound();

            await _workerLogService.DeleteWorkerServiceLog(music);

            return NoContent();
        }


    }
}

