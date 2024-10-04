using Microsoft.AspNetCore.Mvc;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ride_wise_api.Controllers
{
    [Route("motos")]
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
        public async Task<IActionResult> Get([FromQuery] string? placa = null)
        {
            try
            {
                _logger.LogInfo("get motorcycle request");
                var motocycleFilter = new MotorcycleFilter(licensePlate: placa);
                var result = await _motorcycleService.GetAsync(motocycleFilter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro getting motorcycle by license plate {placa}: {ex.Message}");
                return StatusCode(500);
            }
        }

        // GET api/<MotorcyclesController>/5
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetByIdentification([FromRoute] string id)
        {
            try
            {
                _logger.LogInfo($"get motorcycle by identification {id}");
                var filter = new MotorcycleFilter(identification: id);
                var result = await _motorcycleService.GetAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro getting motorcycle by {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        // POST api/<MotorcyclesController>
        [HttpPost]
        public async Task<IActionResult> Post([Required][FromBody] MotorcycleRequest motorcycleRequest)
        {
            try
            {
                _logger.LogInfo($"create motorcycle {motorcycleRequest}");
                var result = await _motorcycleService.CreateAsync(motorcycleRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro creating motorcycle {motorcycleRequest}: {ex.Message}");
                return StatusCode(500);
            }
        }

        // PATCH api/<MotorcyclesController>/5
        [HttpPatch("{identification}/license-plate")]
        public async Task<IActionResult> Put(
            [FromRoute] string identification,
            [FromBody] MotorcycleLicensePlate licensePlate)
        {
            try
            {
                _logger.LogInfo($"update licensePlate from motorcycle identification {identification}");
                var result = await _motorcycleService.UpdateLicensePlateAsync(identification, licensePlate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro updating motorcycle {identification}: {ex.Message}");
                return StatusCode(500);
            }
        }

        // DELETE api/<MotorcyclesController>/5
        [HttpDelete("{identification}")]
        public async Task<IActionResult> Delete([Required][FromRoute] string identification)
        {
            try
            {
                _logger.LogInfo($"delete motorcycle identification {identification}");
                var result = await _motorcycleService.DeleteAsync(identification);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro deleting motorcycle {identification}: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
