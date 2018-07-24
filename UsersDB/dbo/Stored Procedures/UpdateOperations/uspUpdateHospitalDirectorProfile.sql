CREATE PROCEDURE [dbo].[uspUpdateHospitalDirectorProfile]
	@userId int,
	@hospitalName nvarchar(100),
	@occupation nvarchar(100),
	@startedWorking nvarchar(100)
AS
	begin
		declare @profileId int

		select @profileId = UserProfile.ProfileId from UserProfile
			where UserProfile.UserId = @userId and [Type] = 'hospital_director'

		update HospitalDirectors set
			HospitalName = @hospitalName,
			Occupation = @occupation,
			StartedWorking = @startedWorking
			where HospitalDirectors.ProfileId = @profileId

		update UserProfile set
			IsApproved = 0
			where ProfileId = @profileId and UserId = @userId
	end
