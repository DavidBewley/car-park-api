using Core.Interfaces;
using Core.Models;

namespace SpecTests.Helpers
{
    public class DatabaseMockBuilder
    {
        private readonly Mock<ICarParkRepository> _mock = new();
        private readonly List<ParkingSpace> _parkingSpaces = new();
        private readonly List<Booking> _bookings = new();

        public void AddParkingSpace(ParkingSpace space)
            => _parkingSpaces.Add(space);

        public void AddBooking(Booking booking)
            => _bookings.Add(booking);

        public void AddBookingGetById(Booking booking)
            => _mock.Setup(m => m.GetBooking(booking.BookingId)).ReturnsAsync(booking);

        public void AddParkingSpaceGetById(ParkingSpace space)
            => _mock.Setup(m => m.GetParkingSpaceById(space.ParkingSpaceId)).ReturnsAsync(space);

        public Mock<ICarParkRepository> Build()
        {
            _mock.Setup(m => m.GetAllBookings()).ReturnsAsync(_bookings);
            _mock.Setup(m => m.GetAllParkingSpaces()).ReturnsAsync(_parkingSpaces);
            return _mock;
        }
    }
}
