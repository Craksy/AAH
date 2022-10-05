USE Auction_House;

------------------------
-- Create a god user ---
------------------------
BEGIN;
    DROP LOGIN [God];
    DROP USER IF EXISTS [God];
    CREATE LOGIN [God] 
        WITH PASSWORD = 'Generous', 
        DEFAULT_DATABASE = [Auction_House], 
        CHECK_EXPIRATION = OFF, 
        CHECK_POLICY = OFF;
    CREATE USER [God] FOR LOGIN [God];
    ALTER ROLE [db_owner] ADD MEMBER [God];
END;

------------------------------------------------
-- Create a role for the auction users ---------
------------------------------------------------
BEGIN;
    DROP ROLE IF EXISTS [AuctionUser];
    CREATE ROLE AuctionUser;
    GRANT ALTER ON DATABASE::Auction_House TO AuctionUser;
    GRANT SELECT ON DATABASE::Auction_House TO AuctionUser;
    GRANT INSERT ON DATABASE::Auction_House TO AuctionUser;
    GRANT UPDATE ON DATABASE::Auction_House TO AuctionUser;
    GRANT DELETE ON DATABASE::Auction_House TO AuctionUser;
END;

------------------------------------------------
-- Create a procedure to create a new user -----
-- and grant the AuctionUser role to it --------
------------------------------------------------
DROP PROCEDURE IF EXISTS CreateAuctionUser;
BEGIN
    CREATE PROCEDURE CreateAuctionUser(
        @UserName nvarchar(50),
        @Password nvarchar(50),
        @ZipCode nvarchar(10),
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
        SET @Id = SCOPE_IDENTITY();
    END;
END;