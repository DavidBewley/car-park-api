USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[UpdateBooking]
	@BookingId uniqueidentifier,
	@StartDate Date,
	@EndDate Date,
	@ParkingSpaceId uniqueidentifier
AS
BEGIN
	UPDATE dbo.Bookings
	SET StartDate = @StartDate,
	    EndDate = @EndDate
	WHERE BookingId = @BookingId
END