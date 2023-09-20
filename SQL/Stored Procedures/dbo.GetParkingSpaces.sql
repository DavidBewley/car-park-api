USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[GetParkingSpaces]
AS
BEGIN
	SELECT
			ParkingSpaceId,
			Width,
			BayIdentifier
	FROM dbo.ParkingSpaces
END