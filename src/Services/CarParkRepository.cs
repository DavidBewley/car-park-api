using System.Data;
using Dapper;
using System.Data.SqlClient;
using Core.Interfaces;
using Core.Models;
using Serilog;

namespace Services
{
    public class CarParkRepository : ICarParkRepository
    {
        private readonly string _connectionString;

        public CarParkRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateBooking(Booking booking) 
            => await Execute("[dbo].[CreateBooking]",
                new
                {
                    booking.BookingId,
                    booking.StartDate,
                    booking.EndDate,
                    booking.ParkingSpaceId
                });

        public async Task UpdateBooking(Booking booking)
            => await Execute("[dbo].[UpdateBooking]",
                new
                {
                    booking.BookingId,
                    booking.StartDate,
                    booking.EndDate,
                    booking.ParkingSpaceId,
                });

        public async Task DeleteBooking(Guid id)
            => await Execute("[dbo].[DeleteBookingFromId]",
                new
                {
                    BookingId = id,
                });

        public async Task<Booking?> GetBooking(Guid id)
            => (await Query<Booking>("[dbo].[GetBookingFromId]",
                new
                {
                    BookingId = id
                })).FirstOrDefault();

        public async Task<List<Booking>> GetAllBookings()
            => await Query<Booking>("[dbo].[GetBookings]");

        public async Task<List<ParkingSpace>> GetAllParkingSpaces()
            => await Query<ParkingSpace>("[dbo].[GetParkingSpaces]");

        public async Task<ParkingSpace> GetParkingSpaceById(Guid id)
            => (await Query<ParkingSpace>("[dbo].[GetParkingSpaceById]",
                new
                {
                    ParkingSpaceId = id
                })).First();


        private async Task Execute(string procedureName, object? parameters = null)
        {
            try
            {
                Log.Information("EXECUTE {proc} with parameters {parameters}", procedureName, parameters);

                using IDbConnection db = new SqlConnection(_connectionString);
                await db.ExecuteAsync(procedureName, param: parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to Execute Stored Procedure {proc} with parameters {params}, Error: {error}", procedureName, parameters, ex.Message);
                throw;
            }
        }

        private async Task<List<T>> Query<T>(string procedureName, object? parameters = null)
        {
            try
            {
                Log.Information("QUERY {proc} with parameters {parameters}", procedureName, parameters);

                using IDbConnection db = new SqlConnection(_connectionString);
                var results = await db.QueryAsync<T>(procedureName, param: parameters, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception ex)
            {
                Log.Error("Failed to Query Stored Procedure {proc} with parameters {params}, Error: {error}", procedureName, parameters, ex.Message);
                throw;
            }
        }
    }
}
