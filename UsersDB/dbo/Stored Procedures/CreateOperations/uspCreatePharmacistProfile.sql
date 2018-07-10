CREATE PROCEDURE [dbo].[uspCreateDoctor]
	@userId int,
	@createdDate Datetime = GetDate,
	@isApproved bit = 0,
	@pharmacyId int
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'pharmacist', @createdDate, @isApproved 

		insert into PharmacistProfile values(@profileId, @pharmacyId)
	end
