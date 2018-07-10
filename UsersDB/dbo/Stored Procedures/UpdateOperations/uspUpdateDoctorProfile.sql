CREATE PROCEDURE [dbo].[uspUpdateDoctorProfile]
	@userId int,
	@license nvarchar(50) = null,
	@hospitalId int = null
AS
begin
	declare @profileId int

	select @profileId = UserProfile.ProfileId from UserProfile where UserProfile.UserId = @userId

	update DoctorProfile set 
		License = IIF(@license = null, License, @license),
		HospitalId = IIF(@hospitalId = null, HospitalId, @hospitalId)
		where DoctorProfile.ProfileId = @profileId
end
