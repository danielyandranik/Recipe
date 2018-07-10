CREATE PROCEDURE [dbo].[uspCreateDoctorProfile]
	@userId int,
	@createdDate Datetime = GetDate,
	@isApproved bit = 0,
	@license nvarchar(20),
	@hospitalId int
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'doctor', @createdDate, @isApproved 

		insert into DoctorProfile values(@profileId, @license, @hospitalId)
	end
