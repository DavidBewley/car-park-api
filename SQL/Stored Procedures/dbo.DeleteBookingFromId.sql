USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[DeleteBookingFromId]
	@BookingId uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.Bookings
	WHERE BookingId = @BookingId
END