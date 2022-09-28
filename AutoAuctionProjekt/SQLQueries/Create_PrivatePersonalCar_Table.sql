DROP TABLE PrivatePersonalCar
CREATE TABLE PrivatePersonalCar(
	ID INT IDENTITY(1,1) NOT NULL,
	HasIsofix BIT,
	PersonalCarID INT,
	PRIMARY KEY (ID)
);