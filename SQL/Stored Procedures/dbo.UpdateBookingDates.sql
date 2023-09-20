USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[UpdateBookingDates]
	@BookingId uniqueidentifier,
	@StartDate Date,
	@EndDate Date
AS
BEGIN
	UPDATE dbo.Bookings
	SET StartDate = @StartDate,
	    EndDate = @EndDate
	WHERE BookingId = @BookingId
END