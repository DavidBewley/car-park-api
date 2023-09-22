USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[GetBookingFromId]
	@BookingId uniqueidentifier
AS
BEGIN
	SELECT TOP (1)
			book.BookingId,
			book.StartDate,
			book.EndDate,
			book.LinkParkingSpace as 'ParkingSpaceId'
	FROM dbo.Bookings book
	LEFT JOIN ParkingSpaces ps ON ps.ParkingSpaceId = book.LinkParkingSpace
	WHERE book.BookingId = @BookingId
END