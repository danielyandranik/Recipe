CREATE PROCEDURE [dbo].[uspGetUserIdInfoById]
	@userId int
AS
	select Users.Id,
		   Users.Username,
		   Users.[Password],
		   Users.IsActive,
		   Users.CurrentProfileType,
		   Users.IsVerified
		   from Users
		   where Id = @userId