USE CarPark

CREATE TABLE ParkingSpaces
(
	ParkingSpaceId uniqueidentifier NOT NULL,
	Width decimal(5,2) NOT NULL,
	BayText varchar(10),
	PRIMARY KEY (ParkingSpaceId)
)