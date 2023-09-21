using Core.Helpers;
using Core.Models.Requests;
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
        [Route("Price")]
        public IActionResult GetPriceForTimePeriod(PriceRequest request) 
            => Ok(PriceHelper.CalculatePrice(request.StartDate, request.EndDate));
    }
}
