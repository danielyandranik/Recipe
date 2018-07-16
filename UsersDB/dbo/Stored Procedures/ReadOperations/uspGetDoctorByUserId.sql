CREATE PROCEDURE [dbo].[uspGetDoctorByUserId]
@userId int
AS
	select * from UserProfile
	inner join Doctors on UserProfile.ProfileId = Doctors.ProfileId
	where UserProfile.UserId = @userId
