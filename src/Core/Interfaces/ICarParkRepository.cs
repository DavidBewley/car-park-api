using System;
using System.Collections.Generic;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICarParkRepository
    {
        Task CreateBooking(Booking booking);
        Task UpdateBooking(Booking booking);
        Task DeleteBooking(Guid id);
        Task<Booking> GetBooking(Guid id);
        Task<List<Booking>> GetAllBookings();
        Task<List<ParkingSpace>> GetAllParkingSpaces();
    }
}
