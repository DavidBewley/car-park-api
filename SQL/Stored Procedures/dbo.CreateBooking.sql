USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[CreateBooking]
	@BookingId uniqueidentifier,
	@StartDate Date,
	@EndDate Date,
	@ParkingSpaceId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.Bookings (BookingId,StartDate,EndDate,LinkParkingSpace)
	VALUES (@BookingId,@StartDate,@EndDate,@ParkingSpaceId)
END