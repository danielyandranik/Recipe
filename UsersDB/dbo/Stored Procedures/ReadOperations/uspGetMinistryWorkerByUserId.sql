CREATE PROCEDURE [dbo].[uspGetMinistryWorkerByUserId]
	@userId int
AS
	select * from UserProfile
	inner join MinistryWorkers on UserProfile.ProfileId = MinistryWorkers.ProfileId
	where UserProfile.UserId = @userId
