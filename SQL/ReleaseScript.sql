CREATE DATABASE CarPark
GO

USE CarPark

CREATE TABLE ParkingSpaces
(
	ParkingSpaceId uniqueidentifier NOT NULL,
	Width decimal(5,2) NOT NULL,
	BayIdentifier varchar(10),
	PRIMARY KEY (ParkingSpaceId)
)

CREATE TABLE Bookings
(
	BookingId uniqueidentifier NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	LinkParkingSpace uniqueidentifier NOT NULL,
	PRIMARY KEY (BookingId),
	FOREIGN KEY (LinkParkingSpace) REFERENCES ParkingSpaces(ParkingSpaceId)
)

INSERT INTO ParkingSpaces VALUES (NEWID(),2.4,'101')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.5,'102')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.5,'103')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.5,'104')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.4,'105')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.6,'201')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.8,'202')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.6,'203')
INSERT INTO ParkingSpaces VALUES (NEWID(),2.6,'204A')
INSERT INTO ParkingSpaces VALUES (NEWID(),3.0,'204B')

INSERT INTO Bookings VALUES (NEWID(),GETDATE(),DATEADD(DAY,3,GETDATE()),(SELECT TOP (1) ParkingSpaceId FROM ParkingSpaces WHERE BayIdentifier = '101'))
INSERT INTO Bookings VALUES (NEWID(),GETDATE(),DATEADD(DAY,7,GETDATE()),(SELECT TOP (1) ParkingSpaceId FROM ParkingSpaces WHERE BayIdentifier = '201'))
INSERT INTO Bookings VALUES (NEWID(),GETDATE(),DATEADD(DAY,14,GETDATE()),(SELECT TOP (1) ParkingSpaceId FROM ParkingSpaces WHERE BayIdentifier = '202'))
GO

CREATE OR ALTER PROCEDURE [dbo].[GetBookingFromId]
	@BookingId uniqueidentifier
AS
BEGIN
	SELECT TOP (1)
			book.BookingId,
			book.StartDate,
			book.EndDate,
			book.LinkParkingSpace
	FROM dbo.Bookings book
	LEFT JOIN ParkingSpaces ps ON ps.ParkingSpaceId = book.LinkParkingSpace
	WHERE book.BookingId = @BookingId
END
GO

CREATE OR ALTER PROCEDURE [dbo].[DeleteBookingFromId]
	@BookingId uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.Bookings
	WHERE BookingId = @BookingId
END
GO

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
GO

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
GO

CREATE OR ALTER PROCEDURE [dbo].[GetParkingSpaces]
AS
BEGIN
	SELECT
			ParkingSpaceId,
			Width,
			BayIdentifier,
	FROM dbo.ParkingSpaces
END
GO

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