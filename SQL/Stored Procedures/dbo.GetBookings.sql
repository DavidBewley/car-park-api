USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[GetBookings]
AS
BEGIN
	SELECT
			BookingId,
			StartDate,
			EndDate,
			LinkParkingSpace as 'ParkingSpaceId'
	FROM dbo.Bookings
END
GO