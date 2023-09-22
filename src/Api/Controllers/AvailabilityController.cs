using Core.Models.Requests;
using Core.Processors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/availability")]
    public class AvailabilityController : ControllerBase
    {
        private readonly AvailabilityProcessor _availabilityProcessor;

        public AvailabilityController(AvailabilityProcessor availabilityProcessor) 
            => _availabilityProcessor = availabilityProcessor;

        [HttpGet]
        public async Task<IActionResult> GetAvailabilityForTimePeriod([FromQuery] AvailabilityRequest request) 
            => Ok(await _availabilityProcessor.GetAvailabilityForDates(request));
    }
}
