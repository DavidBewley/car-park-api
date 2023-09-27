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
            => _carParkRepository = carParkRepository;

        public async Task GetBooking(Guid bookingId, Action<BookingResponse> onFound, Action<string> onNotFound)
        {
            var (bookingExists, foundBooking) = await TryGetBooking(bookingId, onNotFound);
            if (!bookingExists)
                return;

            onFound(
                new BookingResponse()
                    .FromBooking(foundBooking)
                    .WithParkingSpaceData(await _carParkRepository.GetParkingSpaceById(foundBooking.ParkingSpaceId))
                );
        }

        public async Task CreateBooking(BookingRequest request, Action<BookingResponse> onSuccess, Action<string> onNoAvailability)
        {
            var foundSpace = await FindFreeSpaceForDates(request.StartDate.Date, request.EndDate.Date);
            if (foundSpace == null)
            {
                onNoAvailability(Constants.Messages.NoSpacesAvailable);
                return;
            }

            var booking = new Booking()
                .WithRequest(request)
                .WithParkingSpace(foundSpace.ParkingSpaceId);

            await _carParkRepository.CreateBooking(booking);

            onSuccess(new BookingResponse()
                .FromBooking(booking)
                .WithParkingSpaceData(foundSpace)
            );
        }

        public async Task UpdateBooking(Guid bookingId, BookingRequest request, Action<BookingResponse> onSuccess, Action<string> onNotFound, Action<string> onNoAvailability)
        {
            var (bookingExists, foundBooking) = await TryGetBooking(bookingId, onNotFound);
            if (!bookingExists)
                return;

            var foundSpace = await FindFreeSpaceForDates(request.StartDate.Date, request.EndDate.Date, bookingId);
            if (foundSpace == null)
            {
                onNoAvailability(Constants.Messages.NoSpacesAvailableUpdate);
                return;
            }

            var booking = new Booking()
                .WithRequest(request)
                .WithBookingId(bookingId)
                .WithParkingSpace(foundSpace.ParkingSpaceId);

            await _carParkRepository.UpdateBooking(booking);

            onSuccess(new BookingResponse()
                .FromBooking(booking)
                .WithParkingSpaceData(foundSpace)
            );
        }

        public async Task DeleteBooking(Guid bookingId, Action onSuccess, Action<string> onNotFound)
        {
            var (bookingExists, foundBooking) = await TryGetBooking(bookingId, onNotFound);
            if (!bookingExists)
                return;

            await _carParkRepository.DeleteBooking(bookingId);
            onSuccess();
        }

        private async Task<(bool bookingFound, Booking booking)> TryGetBooking(Guid bookingId, Action<string> failureAction)
        {
            var foundBooking = await _carParkRepository.GetBooking(bookingId);
            if (foundBooking != null)
                return (true, foundBooking);

            failureAction(Constants.Messages.BookingNotFound);
            return (false, default);
        }

        private async Task<ParkingSpace?> FindFreeSpaceForDates(DateTime startDate, DateTime endDate, Guid? excludedBookingRequestId = null)
        {
            var spaces = await _carParkRepository.GetAllParkingSpaces();
            var bookings = await _carParkRepository.GetAllBookings();

            if (excludedBookingRequestId.HasValue)
                bookings = bookings.Where(b => b.BookingId != excludedBookingRequestId.Value).ToList();

            var conflictSpaces = bookings
                .Where(b => b.BookingConflictsWithDates(startDate, endDate))
                .Select(s => s.ParkingSpaceId)
                .ToList();

            return spaces.FirstOrDefault(s => !conflictSpaces.Contains(s.ParkingSpaceId));
        }
    }
}
