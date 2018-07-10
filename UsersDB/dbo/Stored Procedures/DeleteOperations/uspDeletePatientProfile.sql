CREATE PROCEDURE [dbo].[uspDeletePatientProfile]
	@userId int
AS
begin
	declare @profileId int

	select @profileId = ProfileId from UserProfile where UserProfile.UserId = @userId and UserProfile.Type = 'patient'

	delete from PatientProfile where PatientProfile.ProfileId = @profileId

	delete from UserProfile where UserProfile.ProfileId = @profileId

end