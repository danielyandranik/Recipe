CREATE PROCEDURE [dbo].[uspGetDoctorByUserId]
@userId int
AS
	select * from UserProfile
	inner join DoctorProfile on UserProfile.ProfileId = DoctorProfile.ProfileId
	where UserProfile.UserId = @userId
