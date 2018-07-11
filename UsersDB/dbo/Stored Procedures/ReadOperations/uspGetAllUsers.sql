
CREATE PROCEDURE [dbo].[uspGetAllUsers]
AS
	SELECT Id,FirstName,LastName,UserName,Email,Phone,Sex,CurrentProfile
			from dbo.Users
RETURN 0