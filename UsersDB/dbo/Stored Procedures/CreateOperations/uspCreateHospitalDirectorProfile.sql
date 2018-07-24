CREATE PROCEDURE [dbo].[uspCreateHospitalDirectorProfile]
	@userId int,
	@hospitalName nvarchar(100),
	@occupation nvarchar(100),
	@startedWorking nvarchar(100)
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'hospitaldirector'

		if @profileId != 0
			begin
				insert into HospitalDirectors 
					values(@profileId,@hospitalName,@occupation,@startedWorking)
			end
	end
