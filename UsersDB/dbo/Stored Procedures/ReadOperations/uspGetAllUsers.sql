
CREATE PROCEDURE [dbo].[uspGetAllUsers]
AS
	SELECT FirstName,LastName,Username,Email,Phone,Sex,CurrentProfile
			from dbo.Users
RETURN 0