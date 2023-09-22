using System;
using Core.Models.Requests;

namespace Core.Models
{
    public class Booking
    {
        public Guid BookingId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Guid ParkingSpaceId { get; private set; }

        public Booking WithBookingId(Guid id)
        {
            BookingId = id;
            return this;
        }

        public Booking WithRequest(BookingRequest request)
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
