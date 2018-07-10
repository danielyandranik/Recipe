CREATE PROCEDURE [dbo].[uspGetPatientByUserId]
@userId int
AS
	select * from UserProfile
	inner join PatientProfile on UserProfile.ProfileId = PatientProfile.ProfileId
	where UserProfile.UserId = @userId
