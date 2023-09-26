using System;

namespace Core.Models.Responses
{
    public class BookingResponse
    {
        public Guid BookingId { get; private set; }
        public string StartDate { get; private set; }
        public string EndDate { get; private set; }
        public Guid ParkingSpaceId { get; private set; }
        public string? BayIdentifier { get; private set; }
        public decimal BayWidthInMetres { get; private set; }

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
