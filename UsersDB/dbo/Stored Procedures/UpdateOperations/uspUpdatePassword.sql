CREATE PROCEDURE [dbo].[uspUpdatePassword]
	@userId int,
	@oldPassword varchar(100),
	@newPassword varchar(100)
AS
	update Users
		set [Password] = @newPassword
		where Id = @userId and [Password] = @oldPassword