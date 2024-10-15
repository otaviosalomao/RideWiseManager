using Microsoft.AspNetCore.Mvc;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Services.Interfaces;
using RideWise.Common.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RideWise.Api.Controllers
{
    [Route("motos")]
    [Tags("Motos")]
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

        /// <summary>
        /// Buscar motos
        /// </summary>                            
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não Encontrado</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] string? placa = null)
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
                return StatusCode(400, new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Buscar moto por Id
        /// </summary>                            
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não Encontrado</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdentification([FromRoute] string id)
        {
            try
            {
                _logger.LogInfo($"get motorcycle by identification {id}");
                var filter = new MotorcycleFilter(Id: id);
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
                return StatusCode(400, new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cadastrar moto
        /// </summary>                            
        /// <response code="201">Sucesso</response>        
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([Required][FromBody] MotorcycleRequest motorcycleRequest)
        {
            try
            {
                _logger.LogInfo($"create motorcycle {motorcycleRequest}");
                var result = await _motorcycleService.CreateAsync(motorcycleRequest);
                return Created("", result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro creating motorcycle {motorcycleRequest}: {ex.Message}");
                return StatusCode(400, new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar placa por id da moto
        /// </summary>                            
        /// <response code="200">Sucesso</response>        
        /// <response code="400">Dados Inválidos</response>
        [HttpPut("{id}/placa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLicensePlate(
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
                return StatusCode(400, new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Excluir moto
        /// </summary>                            
        /// <response code="200">Sucesso</response>        
        /// <response code="400">Dados Inválidos</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([Required][FromRoute] string id)
        {
            try
            {
                _logger.LogInfo($"delete motorcycle identification {id}");
                var result = await _motorcycleService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro deleting motorcycle {id}: {ex.Message}");
                return StatusCode(400, new { mensagem = ex.Message });
            }
        }
    }
}
