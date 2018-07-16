CREATE PROCEDURE [dbo].[uspUpdateCurrentProfile]
	@userId int,
	@newCurrentProfile nvarchar(20)
AS
	begin
		if exists (select [Type] from UserProfile 
		where UserId = @userId and [Type] = @newCurrentProfile)
			begin
				declare @isApproved bit
				declare @profileId int
				declare @profileType nvarchar(20)

				select @profileId = ProfileId,@profileType = [Type],@isApproved = IsApproved from UserProfile 
						where UserId = @userId and [Type] = @newCurrentProfile
				if (@isApproved = 1)
				begin
					update Users
						set CurrentProfileId = @profileId,
							CurrentProfileType = @profileType
							where Id = @userId
				end
			end		
	end
