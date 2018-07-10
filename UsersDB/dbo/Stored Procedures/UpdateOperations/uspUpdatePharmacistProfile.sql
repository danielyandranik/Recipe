CREATE PROCEDURE [dbo].[uspUpdatePharmacistProfile]
	@userId int,
	@pharmacyId int = null
AS
begin
	declare @profileId int

	select @profileId = UserProfile.ProfileId from UserProfile where UserProfile.UserId = @userId

	update PharmacistProfile set 
		PharmacyId = IIF(@pharmacyId = null, PharmacyId, @pharmacyId)
		where PharmacistProfile.ProfileId = @profileId
end
