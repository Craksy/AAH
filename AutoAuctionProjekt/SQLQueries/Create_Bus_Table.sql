DROP TABLE Bus
CREATE TABLE Bus(
	ID INT IDENTITY(1,1) NOT NULL,
	Seats SMALLINT,
	SleepingSpaces SMALLINT,
	HasToilet BIT,
	DriversLicense VARCHAR(2) CHECK (DriversLicense IN ('A', 'B', 'C', 'D', 'BE', 'CE', 'DE')),
	EngineSize FLOAT,
	HeavyVehicleID INT,
	PRIMARY KEY (ID)
);

/*
ID
Seats
SleepSpaces
HasToilet
DriversLicense
*/