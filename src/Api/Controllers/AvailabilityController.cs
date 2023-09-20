﻿using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetPriceForTimePeriod(DateTime startDate, DateTime endDate)
            => new ContentResult { StatusCode = 501 };
    }
}
