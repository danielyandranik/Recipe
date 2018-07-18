CREATE PROCEDURE [dbo].[uspGetHospitalDirectorByUserId]
	@userId int
AS
	select * from UserProfile
	inner join HospitalDirectors on UserProfile.ProfileId = HospitalDirectors.ProfileId
	where UserProfile.UserId = @userId

