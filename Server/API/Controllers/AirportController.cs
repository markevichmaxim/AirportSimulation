using API.Models.Dto;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Service.Airport.Processing;
using System.Text.Json;

namespace API.Controllers
{
    [Route("airport")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IStateDataSender _stateDataSender;
        private readonly AirportManager _airportManager;
        private readonly DtoMapper _mapper;

        public AirportController(IStateDataSender stateDataSender, AirportManager airportManager, DtoMapper mapper)
        {
            _airportManager = airportManager;
            _stateDataSender = stateDataSender;
            _mapper = mapper;
        }

        [HttpGet("get-state")]
        public IActionResult GetAirportState()
        {
            var json = JsonSerializer.Serialize(_stateDataSender.GetAirportStateImage());

            return Ok(json);
        }

        [HttpPost("send-flight")]
        public async Task<IActionResult> GetFlightAsync([FromBody] FlightDto incomeFlight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _airportManager.AcceptFlightAtAirport(_mapper.DtoToModel(incomeFlight));

            return Ok(incomeFlight);
        }
    }
}
