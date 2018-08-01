CREATE PROCEDURE [dbo].[uspGetPharmacyAdminByUserId]
	@userId int
AS
	select * from UserProfile
	inner join PharmacyAdmins on UserProfile.ProfileId = PharmacyAdmins.ProfileId
	where UserProfile.UserId = @userId
