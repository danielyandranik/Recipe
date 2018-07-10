CREATE PROCEDURE [dbo].[uspGetAllPharmacists]
AS
	select * from UserProfile
	inner join PharmacistProfile on UserProfile.ProfileId = PharmacistProfile.ProfileId

