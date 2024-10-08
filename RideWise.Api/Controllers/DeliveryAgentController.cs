using Microsoft.AspNetCore.Mvc;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RideWise.Api.Controllers
{    
    [Route("entregadores")]
    [Tags("Entregadores")]
    [ApiController]
    public class DeliveryAgentController : ControllerBase
    {
        readonly IDeliveryAgentService _deliveryAgentService;
        readonly ILoggerManager _logger;

        public DeliveryAgentController(
            IDeliveryAgentService deliveryAgentService,
            ILoggerManager loggerManager)
        {
            _logger = loggerManager;
            _deliveryAgentService = deliveryAgentService;
        }
        /// <summary>
        /// Cadastrar um Entregador
        /// </summary> 
        /// <remarks>"tipo_cnh" deve ser: A, B ou AB</remarks>
        /// <param name="deliveryAgentRequest">Dados do Entregador</param>
        /// <returns>Dado cadastrado</returns>    
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([Required][FromBody] DeliveryAgentRequest deliveryAgentRequest)
        {
            try
            {
                _logger.LogInfo($"create delivery agent {deliveryAgentRequest}");
                var result = await _deliveryAgentService.CreateAsync(deliveryAgentRequest);
                return Created("", result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro creating delivery agent {deliveryAgentRequest}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }
        /// <summary>
        /// Atualizar a CNH
        /// </summary>  
        /// <param name="imagem_cnh">Dados CNH</param>         
        /// <response code="200">CNH atualizada</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost("{id}/cnh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDriverLicenseImage([Required][FromBody] DeliveryAgentDriverLicenseRequest imagem_cnh, [Required][FromRoute] string id)
        {
            try
            {
                _logger.LogInfo($"Updating delivery agent {id}");
                await _deliveryAgentService.UpdateDriverLicenseImageAsync(id, imagem_cnh.Imagem_cnh);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro updating delivery agent identification {id}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }
    }
}
