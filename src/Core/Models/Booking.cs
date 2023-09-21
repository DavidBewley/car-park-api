using System;
using Core.Models.Requests;

namespace Core.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ParkingSpaceId { get; set; }

        public Booking WithCreateRequest(CreateBookingRequest request)
        {
            BookingId = Guid.NewGuid();
            StartDate = request.StartDate.Date;
            EndDate = request.EndDate.Date;
            return this;
        }

        public Booking WithParkingSpace(Guid parkingSpaceId)
        {
            ParkingSpaceId = parkingSpaceId;
            return this;
        }

        public bool BookingConflictsWithDates(DateTime startDate, DateTime endDate) 
            => startDate <= EndDate && endDate >= StartDate;
    }
}
