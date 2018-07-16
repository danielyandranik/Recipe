CREATE PROCEDURE [dbo].[uspUpdateDoctorProfile]
	@userId int,
	@license nvarchar(50) = null,
	@hospitalName nvarchar(50),
	@specification nvarchar(50)
AS
begin
	declare @profileId int

	select @profileId = UserProfile.ProfileId from UserProfile 
		where UserProfile.UserId = @userId and [Type] = 'doctor'

	update Doctors set 
		License = IIF(@license = null, License, @license),
		HospitalName = IIF(@hospitalName = null, HospitalName, @hospitalName),
		Specification = IIf(@specification = null,Specification,@specification)		
		where Doctors.ProfileId = @profileId

	update UserProfile  set
		IsApproved = 0 
		where ProfileId = @profileId and UserId = @userId
end
