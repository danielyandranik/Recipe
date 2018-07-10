CREATE PROCEDURE [dbo].[uspGetPharmacistByUserId]
@userId int
AS
	select * from UserProfile
	inner join PharmacistProfile on UserProfile.ProfileId = PharmacistProfile.ProfileId
	where UserProfile.UserId = @userId
