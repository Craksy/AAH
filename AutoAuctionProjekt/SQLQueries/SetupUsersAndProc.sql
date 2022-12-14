------------------------
-- Create a god user ---
------------------------
DROP LOGIN [God];
DROP USER IF EXISTS [God];
CREATE LOGIN [God] 
    WITH PASSWORD = 'Generous', 
    DEFAULT_DATABASE = [Auction_House], 
    CHECK_EXPIRATION = OFF, 
    CHECK_POLICY = OFF;
CREATE USER [God] FOR LOGIN [God];
ALTER ROLE [db_owner] ADD MEMBER [God];
GO;

------------------------------------------------
-- Create a role for the auction users ---------
------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AuctionUser')
    BEGIN
    CREATE ROLE AuctionUser;
    GRANT ALTER ON DATABASE::Auction_House TO AuctionUser;
    GRANT SELECT ON DATABASE::Auction_House TO AuctionUser;
    GRANT INSERT ON DATABASE::Auction_House TO AuctionUser;
    GRANT UPDATE ON DATABASE::Auction_House TO AuctionUser;
    GRANT DELETE ON DATABASE::Auction_House TO AuctionUser;
END
GO;

------------------------------------------------
-- Create a procedure to create a new user -----
-- and grant the AuctionUser role to it --------
------------------------------------------------
DROP PROCEDURE IF EXISTS CreateAuctionUser; GO;
CREATE PROCEDURE CreateAuctionUser(
    @UserName nvarchar(50),
    @Password nvarchar(50),
    @ZipCode nvarchar(10),
    @CprNumber nvarchar(11) = NULL,
    @CvrNumber nvarchar(11) = NULL,
    @Id int OUTPUT)
AS
BEGIN;
    DECLARE @Sql nvarchar(1000);
    -- Apparently, you can't use parameters in a CREATE USER statement
    -- so we have to build the statement dynamically.
    -- Ideally, we would sanitize the input parameters here, as this script would be vulnerable to SQL injection; 
    -- we are blindly executing code based on user input in a privileged context.
    -- fun exercise: lets attack our own database.
    SET @Sql =
                'CREATE LOGIN ' + @UserName +
                ' WITH PASSWORD = ''' + @Password + ''', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF; ' +
                'CREATE USER ' + @UserName +
                ' FOR LOGIN ' + @UserName + ';' +
                'EXEC sp_addrolemember ''AuctionUser'', ''' + @UserName + ''';';
    EXEC sp_executesql @Sql;
    INSERT INTO Users (UserName, ZipCode, Balance) VALUES (@UserName, @ZipCode, 0);
    IF(@CprNumber IS NOT NULL)
        BEGIN
            INSERT INTO PrivateUsers (Id, CprNumber) VALUES (SCOPE_IDENTITY(), @CprNumber);
        end
    ELSE IF(@CvrNumber IS NOT NULL)
        BEGIN
            INSERT INTO CorporateUsers (Id, CvrNumber, CreditScore) VALUES (SCOPE_IDENTITY(), @CvrNumber, 0);
        end
        
    SET @Id = SCOPE_IDENTITY();
END;