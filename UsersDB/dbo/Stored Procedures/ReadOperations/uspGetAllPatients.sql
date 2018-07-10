CREATE PROCEDURE [dbo].[uspGetAllPatients]
AS
	select * from UserProfile
	inner join PatientProfile on UserProfile.ProfileId = PatientProfile.ProfileId

