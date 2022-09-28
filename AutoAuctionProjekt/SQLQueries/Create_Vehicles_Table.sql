DROP TABLE Vehicles
CREATE TABLE Vehicles (
	ID INT IDENTITY(1,1) NOT NULL,
	Name VARCHAR(15),
	Kilometers FLOAT,
	RegistrationNumber VARCHAR(20),
	Year SMALLINT,
	NewPrice INT,
	HasTowbar BIT,
	EngineSize FLOAT,
	KmPerLiter FLOAT,
	FuelType VARCHAR(10) CHECK (FuelType IN ('Diesel', 'Benzin', 'Electric', 'Hydrogen')),
	EnergyClass VARCHAR(2) CHECK (EnergyClass IN ('A', 'B', 'C', 'D')),
	PRIMARY KEY (ID)
);