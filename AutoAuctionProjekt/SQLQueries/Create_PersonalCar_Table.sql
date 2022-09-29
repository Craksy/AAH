DROP TABLE PersonalCar
CREATE TABLE PersonalCar(
	ID INT IDENTITY(1,1) NOT NULL,
	Seats SMALLINT,
	TrunkHeight FLOAT,
	TrunkWidth FLOAT,
	TrunkDepth FLOAT,
	DriversLicense VARCHAR(2) CHECK (DriversLicense IN ('A', 'B', 'C', 'D', 'BE', 'CE', 'DE')),
	EngineSize FLOAT,
	VehicleID INT,
	PRIMARY KEY (ID)
);