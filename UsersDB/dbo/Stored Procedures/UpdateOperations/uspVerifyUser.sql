CREATE PROCEDURE [dbo].[uspVerifyUser]
	@username nvarchar(50),
	@key varchar(50)
AS
	begin
		delete from Verifications
			where Username= @username and [Key] = @key
		if @@rowcount = 1
			begin
			update Users
				set VerifiedDate = GetDate(),
					IsVerified = 1
				where Username = @username
			end
	end
