

CREATE PROCEDURE [dbo].[uspGetUserById]
	@userID int
AS
	SELECT * from dbo.Users where Users.Id = @userID
RETURN 0