DROP TABLE Busses
CREATE TABLE Busses(
	ID INT IDENTITY(1,1) NOT NULL,
	Seats SMALLINT,
	SleepingSpaces SMALLINT,
	HasToilet BIT,
	DriversLicense VARCHAR(2) CHECK (DriversLicense IN ('A', 'B', 'C', 'D', 'BE', 'CE', 'DE')),
	EngineSize FLOAT,
	HeavyVehicleID INT,
	PRIMARY KEY (ID)
);
INSERT INTO Busses
VALUES (13, 2, 1, 'DE', 10, 1)

/*
ID
Seats
SleepSpaces
HasToilet
DriversLicense
*/