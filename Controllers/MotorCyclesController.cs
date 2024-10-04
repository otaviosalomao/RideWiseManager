using Microsoft.AspNetCore.Mvc;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ride_wise_api.Controllers
{
    [Route("motorcycles")]
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        readonly IMotorcycleService _motorcycleService;
        readonly ILoggerManager _logger;

        public MotorcyclesController(
            IMotorcycleService motorCycleService,
            ILoggerManager loggerManager)
        {
            _logger = loggerManager;
            _motorcycleService = motorCycleService;
        }

        // GET: api/<MotorcyclesController>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Get([FromQuery] MotorcycleFilter filters)
        {
            _logger.LogInfo("get motorcycle request");
            var result = await _motorcycleService.GetAsync(filters);
            return Ok(result);
        }

        // GET api/<MotorcyclesController>/5
        [HttpGet("{identification}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetByIdentification([FromRoute] string identification)
        {
            _logger.LogInfo($"get motorcycle by identification {identification}");
            var filter = new MotorcycleFilter(identification: identification);
            var result = await _motorcycleService.GetAsync(filter);
            return Ok(result);
        }

        // POST api/<MotorcyclesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotorcycleRequest motorcycleRequest)
        {
            _logger.LogInfo($"create motorcycle {motorcycleRequest}");
            var result = await _motorcycleService.CreateAsync(motorcycleRequest);
            return Ok(result);
        }

        // PUT api/<MotorcyclesController>/5
        [HttpPatch("{identification}/license-plate")]
        public async Task<IActionResult> Put([FromRoute] string identification, [FromBody] MotorcycleLicensePlate licensePlate)
        {
            _logger.LogInfo($"update licensePlate from motorcycle identification {identification}");
            var result = await _motorcycleService.UpdateLicensePlateAsync(identification, licensePlate);
            return Ok(result);
        }

        // DELETE api/<MotorcyclesController>/5
        [HttpDelete("{identification}")]
        public async Task<IActionResult> Delete([Required][FromRoute] string identification)
        {
            _logger.LogInfo($"delete motorcycle identification {identification}");
            var result = await _motorcycleService.DeleteAsync(identification);
            return Ok(result);
        }
    }
}
