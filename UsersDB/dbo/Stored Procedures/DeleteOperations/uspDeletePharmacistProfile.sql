CREATE PROCEDURE [dbo].[uspDeletePharmacistProfile]
	@userId int
AS
begin
	declare @profileId int

	select @profileId = ProfileId from UserProfile where UserProfile.UserId = @userId and UserProfile.Type = 'pharmacist'

	delete from PharmacistProfile where PharmacistProfile.ProfileId = @profileId

	delete from UserProfile where UserProfile.ProfileId = @profileId

end