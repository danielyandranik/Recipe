CREATE PROCEDURE [dbo].[uspDeletePatientProfile]
	@userId int
AS
begin
	declare @profileId int

	select @profileId = ProfileId from UserProfile where UserProfile.UserId = @userId and UserProfile.Type = 'patient'

	delete from Patients where Patients.ProfileId = @profileId

	delete from UserProfile where UserProfile.ProfileId = @profileId

end