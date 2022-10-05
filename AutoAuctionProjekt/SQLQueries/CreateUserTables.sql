BEGIN;
    DROP TABLE IF EXISTS PrivateUsers;
    DROP TABLE IF EXISTS CorporateUsers;
    DROP TABLE IF EXISTS Users;
END;

BEGIN;
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        UserName VARCHAR(255) NOT NULL,
        ZipCode VARCHAR(5) NOT NULL,
        Balance INT NOT NULL
    );
    INSERT INTO Users (UserName, ZipCode, Balance) 
    VALUES ('RegularJoe', '9900', 100);
    
    CREATE TABLE PrivateUsers (
        Id INT NOT NULL UNIQUE PRIMARY KEY REFERENCES Users(id),
        CprNumber VARCHAR(11) NOT NULL,
    );

    CREATE TABLE CorporateUsers (
        Id INT NOT NULL UNIQUE PRIMARY KEY REFERENCES Users(id), 
        CvrNumber VARCHAR(8) NOT NULL,
        CreditScore INT NOT NULL,
    );
END;