using System;
using Core.Models.Requests;

namespace Core.Models.Responses
{
    public class PriceResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public PriceResponse() { }

        public PriceResponse(AvailabilityRequest request, decimal price)
        {
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            Price = price;
        }
    }
}
