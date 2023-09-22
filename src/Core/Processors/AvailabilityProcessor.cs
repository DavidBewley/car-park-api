using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;
using Core.Models.Responses;

namespace Core.Processors
{
    public class AvailabilityProcessor
    {
        private readonly ICarParkRepository _carParkRepository;

        public AvailabilityProcessor(ICarParkRepository carParkRepository)
        {
            _carParkRepository = carParkRepository;
        }

        public async Task<AvailabilityResponse> GetAvailabilityForDates(AvailabilityRequest request) 
            => new AvailabilityResponse()
                .WithRequestData(request)
                .WithPrice(PriceHelper.CalculatePrice(request.StartDate, request.EndDate))
                .WithAvailableSpaces(await FindAllFreeSpaceForDates(request.StartDate, request.EndDate));

        private async Task<List<ParkingSpace>> FindAllFreeSpaceForDates(DateTime startDate, DateTime endDate)
        {
            var spaces = await _carParkRepository.GetAllParkingSpaces();

            var conflictSpaces = (await _carParkRepository.GetAllBookings())
                .Where(b => b.BookingConflictsWithDates(startDate, endDate))
                .Select(s => s.ParkingSpaceId)
                .ToList();

            return spaces.Where(s => !conflictSpaces.Contains(s.ParkingSpaceId)).ToList();
        }
    }
}
