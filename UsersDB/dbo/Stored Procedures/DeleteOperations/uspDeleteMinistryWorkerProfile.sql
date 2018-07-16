CREATE PROCEDURE [dbo].[uspDeleteMinistryWorkerProfile]
	@userId int
AS
	begin
		declare @profileId int

		select @profileId = ProfileId from UserProfile
			where UserProfile.UserId = @userId and [Type] = 'ministryworker'
		
		delete from MinistryWorkers where ProfileId = @profileId
		delete from UserProfile where UserProfile.ProfileId = @profileId
	end
