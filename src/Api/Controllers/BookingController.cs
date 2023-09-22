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
        {
            ActionResult response = NotFound();

            await _bookingProcessor.GetBooking(
                bookingId,
                onFound: booking => response = Ok(booking),
                onNotFound: message => response = NotFound(message)
            );

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            ActionResult response = NotFound();

            await _bookingProcessor.CreateBooking(
                request,
                onSuccess: booking => response = Ok(booking),
                onNoAvailability: message => response = BadRequest(message)
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
        {
            ActionResult response = NotFound();

            await _bookingProcessor.DeleteBooking(
                bookingId,
                onSuccess: () => response = Ok(),
                onNotFound: message => response = BadRequest(message)
            );

            return response;
        }
    }
}
