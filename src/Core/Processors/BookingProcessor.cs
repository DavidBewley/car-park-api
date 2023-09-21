using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;
using Core.Models.Responses;

namespace Core.Processors
{
    public class BookingProcessor
    {
        private readonly ICarParkRepository _carParkRepository;
        public BookingProcessor(ICarParkRepository carParkRepository)
        {
            _carParkRepository = carParkRepository;
        }

        public async Task CreateBookingResponse(CreateBookingRequest request, Action<BookingResponse> onSuccess, Action<string> onNoAvailability)
        {
            var foundSpace = await FindFreeSpaceForDates(request.StartDate.Date, request.EndDate.Date);
            if (foundSpace == null)
            {
                onNoAvailability(Constants.Messages.NoSpacesAvailable);
                return;
            }

            var booking = new Booking()
                .WithCreateRequest(request)
                .WithParkingSpace(foundSpace.ParkingSpaceId);

            await _carParkRepository.CreateBooking(booking);

            onSuccess(new BookingResponse()
                .FromBooking(booking)
                .WithParkingSpaceData(foundSpace)
            );
        }

        private async Task<ParkingSpace?> FindFreeSpaceForDates(DateTime startDate, DateTime endDate)
        {
            var spaces = await _carParkRepository.GetAllParkingSpaces();

            var conflictSpaces = (await _carParkRepository.GetAllBookings())
                .Where(b => b.BookingConflictsWithDates(startDate, endDate))
                .Select(s=>s.ParkingSpaceId)
                .ToList();

            return spaces.FirstOrDefault(s => !conflictSpaces.Contains(s.ParkingSpaceId));
        }
    }
}
