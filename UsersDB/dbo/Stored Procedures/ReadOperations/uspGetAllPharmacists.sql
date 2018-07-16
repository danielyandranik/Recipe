CREATE PROCEDURE [dbo].[uspGetAllPharmacists]
AS
	select * from UserProfile
	inner join Pharmacists on UserProfile.ProfileId = Pharmacists.ProfileId

