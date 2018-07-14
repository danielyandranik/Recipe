CREATE PROCEDURE [dbo].[uspGetAllDoctors]
AS
	select * from UserProfile
	inner join Doctors on UserProfile.ProfileId = Doctors.ProfileId

