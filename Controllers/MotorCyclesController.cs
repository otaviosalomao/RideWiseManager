using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Services.Interfaces;
using ride_wise_api.Application.Validators;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Security.Claims;

namespace ride_wise_api.Controllers
{
    [Route("motos")]
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        readonly IMotorcycleService _motorcycleService;
        readonly ILoggerManager _logger;

        public MotorcyclesController(
            IMotorcycleService motorcycleService,
            ILoggerManager loggerManager)
        {
            _logger = loggerManager;
            _motorcycleService = motorcycleService;
        }

        // GET: api/<MotorcyclesController>
        [HttpGet]        
        public async Task<IActionResult> Get([FromQuery] string? placa = null)
        {
            try
            {
                _logger.LogInfo("get motorcycle request");
                var motocycleFilter = new MotorcycleFilter(licensePlate: placa);
                var result = await _motorcycleService.GetAsync(motocycleFilter);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound(new { mensagem = "Moto(s) não encontrada(s)" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro getting motorcycle by license plate {placa}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }

        // GET api/<MotorcyclesController>/5
        [HttpGet("{id}")]              
        public async Task<IActionResult> GetByIdentification([FromRoute] string id)
        {
            try
            {
                _logger.LogInfo($"get motorcycle by identification {id}");
                var filter = new MotorcycleFilter(identification: id);
                var result = await _motorcycleService.GetAsync(filter);
                if (result.Any())
                {
                    return Ok(result?.FirstOrDefault());
                }
                return NotFound(new { mensagem = "Moto não encontrada" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro getting motorcycle by {id}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
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
                return Created("",result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro creating motorcycle {motorcycleRequest}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }

        // PUT api/<MotorcyclesController>/5
        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Put(
            [Required][FromRoute] string id,
            [FromBody] MotorcycleLicensePlate licensePlate)
        {
            try
            {
                _logger.LogInfo($"update licensePlate from motorcycle identification {id}");
                await _motorcycleService.UpdateLicensePlateAsync(id, licensePlate.Placa);
                return Ok(new { mensagem = "Placa modificada com sucesso" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro updating motorcycle {id}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
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
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro deleting motorcycle {identification}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }
    }
}
