using Core.Models.Requests;
using System;
using System.Collections.Generic;

namespace Core.Models.Responses
{
    public class AvailabilityResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public List<ParkingSpace> AvailableSpaces { get; set; }

        public AvailabilityResponse() { }

        public AvailabilityResponse WithPrice(decimal price)
        {
            Price = price;
            return this;
        }

        public AvailabilityResponse WithRequestData(AvailabilityRequest request)
        {
            StartDate  = request.StartDate;
            EndDate = request.EndDate;
            return this;
        }

        public AvailabilityResponse WithAvailableSpaces(List<ParkingSpace> spaces)
        {
            AvailableSpaces = spaces;
            return this;
        }
    }
}
