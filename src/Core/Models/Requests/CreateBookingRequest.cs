using System;

namespace Core.Models.Requests
{
    public class CreateBookingRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}