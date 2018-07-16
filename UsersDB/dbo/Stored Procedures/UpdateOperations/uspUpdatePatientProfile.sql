CREATE PROCEDURE [dbo].[uspUpdatePatientProfile]
	@userId int,
	@regionalDoctorName nvarchar(50),
	@address nvarchar(50),
	@occupation nvarchar(50),
	@isAlcoholic nvarchar(50),
	@isDrugAddicted nvarchar(50)
AS
begin
	declare @profileId int

	select @profileId = UserProfile.ProfileId from UserProfile where UserProfile.UserId = @userId

	update Patients
		set Patients.RegionalDoctorName = @regionalDoctorName,
			Patients.[Address] = @address,
			Patients.Occupation = @occupation,
			Patients.IsAlcohоlic = @isAlcoholic,
			Patients.IsDrugAddicted = @isDrugAddicted
		where Patients.ProfileId = @profileId
end
