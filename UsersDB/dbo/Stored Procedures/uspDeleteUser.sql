

CREATE PROCEDURE [dbo].[uspDeleteUser]
	@userId int
AS
	delete from dbo.Users where Users.Id = @userId