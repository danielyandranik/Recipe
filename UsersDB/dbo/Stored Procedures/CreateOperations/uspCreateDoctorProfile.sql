CREATE PROCEDURE [dbo].[uspCreateDoctorProfile]
	@userId int,
	@license nvarchar(20),
	@hospitalName nvarchar(50),
	@specification nvarchar(100),
	@workStartYear nvarchar(50),
	@graduatedYear nvarchar(50)
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'doctor'

		insert into Doctors values(@profileId, @license, @hospitalName,@specification,@workStartYear,@graduatedYear)
	end
