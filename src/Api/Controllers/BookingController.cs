using Core.Models.Requests;
using Core.Processors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly BookingProcessor _bookingProcessor;
        public BookingController(BookingProcessor bookingProcessor)
        {
            _bookingProcessor = bookingProcessor;
        }

        [HttpGet]
        [Route("{bookingId}")]
        public async Task<IActionResult> GetBooking([FromRoute] Guid bookingId) 
            => new ContentResult { StatusCode = 501 };

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            ActionResult response = NotFound();

            await _bookingProcessor.CreateBookingResponse(
                request,
                onSuccess: booking => response = Ok(booking),
                onNoAvailability: message => response = Ok(message)
            );

            return response;
        }

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
