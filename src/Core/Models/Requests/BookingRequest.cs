using System;

namespace Core.Models.Requests
{
    public class BookingRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}