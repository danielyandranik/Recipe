CREATE PROCEDURE [dbo].[uspDeleteHospitalDirectorProfile]
	@userId int
AS
	begin
		declare @profileId int

		select @profileId = ProfileId from UserProfile 
			where UserProfile.UserId = @userId and UserProfile.[Type] = 'hospitaldirector'

		delete from HospitalDirectors where HospitalDirectors.ProfileId = @profileId

		delete from UserProfile where UserProfile.ProfileId = @profileId
	end
