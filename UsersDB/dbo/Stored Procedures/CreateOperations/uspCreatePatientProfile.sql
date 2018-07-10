CREATE PROCEDURE [dbo].[uspCreatePatientProfile]
	@userId int,
	@createdDate Datetime = GetDate,
	@isApproved bit = 0
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'patient', @createdDate, @isApproved 

		insert into PatientProfile values(@profileId)
	end
