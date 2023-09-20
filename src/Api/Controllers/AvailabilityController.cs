using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/availability")]
    public class AvailabilityController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAvailabilityForTimePeriod(DateTime startDate, DateTime endDate)
            => new ContentResult { StatusCode = 501 };

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAvailabilityForDate(DateTime startDate)
            => new ContentResult { StatusCode = 501 };
    }
}
