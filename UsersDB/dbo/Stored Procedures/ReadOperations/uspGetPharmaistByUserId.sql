CREATE PROCEDURE [dbo].[uspGetPharmacistByUserId]
@userId int
AS
	select * from UserProfile
	inner join Pharmacists on UserProfile.ProfileId = Pharmacists.ProfileId
	where UserProfile.UserId = @userId
