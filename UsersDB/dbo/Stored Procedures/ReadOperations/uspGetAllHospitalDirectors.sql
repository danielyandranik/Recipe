CREATE PROCEDURE [dbo].[uspGetAllHospitalDirectors]	
AS
	select * from UserProfile
	inner join  HospitalDirectors on UserProfile.ProfileId = HospitalDirectors.ProfileId
