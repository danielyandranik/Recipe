CREATE PROCEDURE [dbo].[uspUpdateMinistryWorkerProfile]
	@userId int,
	@position nvarchar(50),
	@startedWorking nvarchar(50)
AS
	begin
		declare @profileId int
		
		select @profileId = UserProfile.ProfileId 
			from UserProfile where UserId = @userId and [Type] = 'ministry_worker'
		
		update MinistryWorkers set
			Position = @position,
			StartedWorking = @startedWorking

		update UserProfile set
			IsApproved = 0
			where ProfileId = @profileId and UserId = @userId
	end
