CREATE PROCEDURE [dbo].[uspCreatePatientProfile]
	@userId int,
	@regionalDoctorName nvarchar(50),
	@occupation nvarchar(50),
	@address nvarchar(50),
	@isAlcoholic nvarchar(50),
	@isDrugAddicted nvarchar(50)
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'patient'

		if @profileId != 0
			begin
			update UserProfile
				set UserProfile.IsApproved = 1
				where UserProfile.ProfileId = @profileId and UserProfile.UserId = @userId

			insert into Patients
				values(@profileId,@regionalDoctorName,@occupation,@address,@isAlcoholic,@isDrugAddicted)
			end
	end
