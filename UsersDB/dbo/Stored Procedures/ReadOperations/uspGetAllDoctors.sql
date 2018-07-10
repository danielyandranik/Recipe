CREATE PROCEDURE [dbo].[uspGetAllDoctors]
AS
	select * from UserProfile
	inner join DoctorProfile on UserProfile.ProfileId = DoctorProfile.ProfileId

