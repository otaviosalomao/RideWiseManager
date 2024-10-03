using Microsoft.AspNetCore.Mvc;
using ride_wise_api.Application.Models;
using ride_wise_api.Application.Services.Interfaces;
using System.Net;
using System.Net.Mime;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ride_wise_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorCyclesController : ControllerBase
    {
        readonly IMotorCycleService _motorCycleService;
        readonly ILoggerManager _logger;

        public MotorCyclesController(
            IMotorCycleService motorCycleService,
            ILoggerManager loggerManager)
        {
            _logger = loggerManager;
            _motorCycleService = motorCycleService;
        }

        // GET: api/<MotorCyclesController>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]        
        public async Task<IActionResult> Get(string licensePlate)
        {
            _logger.LogInfo("get motorcycle request");
            var result = await _motorCycleService.GetAsync(licensePlate);
            return Ok(result);
        }

        // GET api/<MotorCyclesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MotorCyclesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MotorCyclesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MotorCyclesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
