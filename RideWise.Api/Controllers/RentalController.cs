using Microsoft.AspNetCore.Mvc;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Services;
using RideWise.Api.Application.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RideWise.Api.Controllers
{
    [Route("locacao")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        readonly IRentalService _rentalService;
        readonly ILoggerManager _logger;

        public RentalController(
            IRentalService rentalService,
            ILoggerManager loggerManager)
        {
            _logger = loggerManager;
            _rentalService = rentalService;
        }
        // POST
        [HttpPost]
        public async Task<IActionResult> Create([Required][FromBody] RentalRequest rentalRequest)
        {
            try
            {
                _logger.LogInfo($"create rental {rentalRequest}");
                var result = await _rentalService.CreateAsync(rentalRequest);
                return Created("", result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro creating rental {rentalRequest}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }
        // GET 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdentification([FromRoute] string id)
        {
            try
            {
                _logger.LogInfo($"get rental by identification {id}");
                var filter = new RentalFilter(identification: id);
                var result = await _rentalService.GetAsync(filter);
                if (result is not null)
                {
                    return Ok(result);
                }
                return NotFound(new { mensagem = "Locação não encontrada" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro getting rental by {id}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }
        // PUT 
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> UpdateByDevolutionDate(
            [Required][FromRoute] string id,
            [FromBody] RentalDevolutionDate devolutionDate)
        {
            try
            {
                _logger.LogInfo($"update devolutionDate from rental identification {id}");
                await _rentalService.UpdateDevolutionDateAsync(id, devolutionDate.Data_devolucao);
                return Ok(new { mensagem = "Data de devolução modificada com sucesso" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro updating rental {id}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }
    }
}
