CREATE PROCEDURE [dbo].[uspGetUserPublicInfoByUsername]
	@username nvarchar(50)
AS
	select Id,FirstName,LastName,Username,Sex
		from Users where Username = @username
