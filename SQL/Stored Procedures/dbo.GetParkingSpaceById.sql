USE CarPark

CREATE OR ALTER PROCEDURE [dbo].[GetParkingSpaceById]
	@ParkingSpaceId uniqueidentifier
AS
BEGIN
	SELECT
			ParkingSpaceId,
			Width,
			BayIdentifier
	FROM dbo.ParkingSpaces
	WHERE ParkingSpaceId = @ParkingSpaceId
END