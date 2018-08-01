CREATE PROCEDURE [dbo].[uspDeletePharmacyAdminProfile]
	@userId int
AS
	begin
		declare @profileId int

		select @profileId = ProfileId from UserProfile where UserProfile.UserId = @userId and UserProfile.[Type] = 'pharmacy_admin'

		delete from PharmacyAdmins where PharmacyAdmins.ProfileId = @profileId

		delete from UserProfile where UserProfile.ProfileId = @profileId
	
		exec uspConfigureUserAfterProfileDeletion @userId
	end
