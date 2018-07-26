CREATE PROCEDURE [dbo].[uspGetUserPublicInfoById]
	@id int
AS
	select Id,FirstName,LastName,Username,Sex,CurrentProfileType
		from Users where Id = @id
