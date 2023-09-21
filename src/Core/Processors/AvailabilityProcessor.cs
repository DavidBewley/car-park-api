using Core.Helpers;
using Core.Models.Requests;
using Core.Models.Responses;

namespace Core.Processors
{
    public class AvailabilityProcessor
    {
        public AvailabilityProcessor()
        {
            
        }

        public PriceResponse CalculatePriceForDates(PriceRequest request) 
            => new PriceResponse(request, PriceHelper.CalculatePrice(request.StartDate, request.EndDate));
    }
}
