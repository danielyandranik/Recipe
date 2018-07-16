CREATE PROCEDURE [dbo].[uspGetAllPatients]
AS
	select * from UserProfile
	inner join Patients on UserProfile.ProfileId = Patients.ProfileId

