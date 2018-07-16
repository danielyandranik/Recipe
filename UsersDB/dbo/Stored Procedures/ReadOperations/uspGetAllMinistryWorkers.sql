CREATE PROCEDURE [dbo].[uspGetAllMinistryWorkers]
AS
	select * from UserProfile
	inner join MinistryWorkers on UserProfile.ProfileId = MinistryWorkers.ProfileId
