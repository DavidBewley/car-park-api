USE CarPark

CREATE TABLE Bookings
(
	BookingId uniqueidentifier NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	LinkParkingSpace uniqueidentifier NOT NULL,
	PRIMARY KEY (BookingId),
	FOREIGN KEY (LinkParkingSpace) REFERENCES ParkingSpaces(ParkingSpaceId)
)