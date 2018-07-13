CREATE PROCEDURE [dbo].[uspGetUserPublicInfoById]
	@id int
AS
	select Id,FirstName,LastName,Username,Sex
		from Users where Id = @id
