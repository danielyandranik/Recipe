CREATE PROCEDURE [dbo].[uspConfigureUser]
	@userId int
	AS
	begin
			begin
				update Users
					set CurrentProfileType = 'none',
						CurrentProfileId = 0
						where Id = @userId
			end
	end
