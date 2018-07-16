CREATE PROCEDURE [dbo].[uspDeleteDoctorProfile]
	@userId int
AS
begin
	declare @profileId int

	select @profileId = ProfileId from UserProfile where UserProfile.UserId = @userId and UserProfile.[Type] = 'doctor'

	delete from Doctors where Doctors.ProfileId = @profileId

	delete from UserProfile where UserProfile.ProfileId = @profileId

end