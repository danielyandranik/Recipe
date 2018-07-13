CREATE PROCEDURE [dbo].[uspGetUserIdInfoByUsername]
	@username nvarchar(50)
AS
	select Users.Id,
		   Users.Username,
		   Users.[Password],
		   Users.IsActive,
		   Users.CurrentProfileType,
		   Users.IsVerified
		   from Users
		   where Username = @username
