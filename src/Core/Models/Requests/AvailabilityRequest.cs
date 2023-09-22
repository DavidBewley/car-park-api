using System;

namespace Core.Models.Requests
{
    public class AvailabilityRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
