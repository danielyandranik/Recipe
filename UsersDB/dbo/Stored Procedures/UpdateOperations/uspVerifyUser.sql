CREATE PROCEDURE [dbo].[uspVerifyUser]
	@username nvarchar(100)
AS
	update Users
		set IsVerified = 1,
			VerifiedDate = GetDate()
	where Username = @username
