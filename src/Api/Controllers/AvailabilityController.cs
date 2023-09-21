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
        {
            _availabilityProcessor = availabilityProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailabilityForTimePeriod(DateTime startDate, DateTime endDate)
            => new ContentResult { StatusCode = 501 };

        [HttpGet]
        [Route("Price")]
        public IActionResult GetPriceForTimePeriod([FromQuery] PriceRequest request) 
            => Ok(_availabilityProcessor.CalculatePriceForDates(request));
    }
}
