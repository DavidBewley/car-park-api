using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        [HttpGet]
        [Route("{bookingId}")]
        public async Task<IActionResult> GetBooking([FromRoute] Guid bookingId) 
            => new ContentResult { StatusCode = 501 };

        [HttpPost]
        public async Task<IActionResult> CreateBooking()
            => new ContentResult { StatusCode = 501 };

        [HttpPut]
        [Route("{bookingId}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid bookingId)
            => new ContentResult { StatusCode = 501 };

        [HttpDelete]
        [Route("{bookingId}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid bookingId)
            => new ContentResult { StatusCode = 501 };
    }
}
