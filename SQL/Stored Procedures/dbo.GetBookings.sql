USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[GetBookings]
AS
BEGIN
	SELECT
			BookingId,
			StartDate,
			EndDate,
			LinkParkingSpace
	FROM dbo.Bookings
END
GO