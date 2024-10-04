using Microsoft.AspNetCore.Mvc;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ride_wise_api.Controllers
{
    [Route("entregadores")]
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
        // POST api/<MotorcyclesController>
        [HttpPost]
        public async Task<IActionResult> Post([Required][FromBody] MotorcycleRequest motorcycleRequest)
        {
            try
            {
                _logger.LogInfo($"create delivery agent {motorcycleRequest}");
                var result = await _deliveryAgentService.CreateAsync(motorcycleRequest);
                return Created("", result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro creating delivery agent {motorcycleRequest}: {ex.Message}");
                return StatusCode(400, new { mensagem = "Dados inválidos" });
            }
        }
    }
}
