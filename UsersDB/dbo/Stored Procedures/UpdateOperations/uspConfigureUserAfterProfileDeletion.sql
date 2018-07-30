CREATE PROCEDURE [dbo].[uspConfigureUserAfterProfileDeletion]
	@userId int
	AS
	begin
		if not exists (select UserProfile.ProfileId from UserProfile where UserId = @userId)
			begin
				update Users
					set CurrentProfileType = 'None',
						CurrentProfileId = 0
						where Id = @userId
			end
	end
