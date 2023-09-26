using Core.Models.Requests;
using System.Collections.Generic;

namespace Core.Models.Responses
{
    public class AvailabilityResponse
    {
        public string StartDate { get; private set; }
        public string EndDate { get; private set; }
        public decimal Price { get; private set; }
        public List<ParkingSpace> AvailableSpaces { get; private set; } = new List<ParkingSpace>();

        public AvailabilityResponse WithPrice(decimal price)
        {
            Price = price;
            return this;
        }

        public AvailabilityResponse WithRequestData(AvailabilityRequest request)
        {
            StartDate  = request.StartDate.ToShortDateString();
            EndDate = request.EndDate.ToShortDateString();
            return this;
        }

        public AvailabilityResponse WithAvailableSpaces(List<ParkingSpace> spaces)
        {
            AvailableSpaces = spaces;
            return this;
        }
    }
}
