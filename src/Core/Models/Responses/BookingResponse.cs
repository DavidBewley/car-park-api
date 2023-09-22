using System;

namespace Core.Models.Responses
{
    public class BookingResponse
    {
        public Guid BookingId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Guid ParkingSpaceId { get; set; }
        public string? BayIdentifier { get; set; }
        public decimal BayWidthInMetres { get; set; }

        public BookingResponse FromBooking(Booking booking)
        {
            BookingId = booking.BookingId;
            StartDate = booking.StartDate.ToShortDateString();
            EndDate = booking.EndDate.ToShortDateString();
            ParkingSpaceId = booking.ParkingSpaceId;
            return this;
        }

        public BookingResponse WithParkingSpaceData(ParkingSpace space)
        {
            BayIdentifier = space.BayIdentifier;
            BayWidthInMetres = space.Width;
            return this;
        }
    }
}
