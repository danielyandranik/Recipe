CREATE PROCEDURE [dbo].[uspCreatePharmacistProfile]
	@userId int,
	@pharmacyId int,
	@pharmacyName nvarchar(100),
	@startedWorking nvarchar(50)
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'pharmacist'

		if @profileId != 0
			begin
				insert into Pharmacists values(@profileId, @pharmacyId, @pharmacyName,@startedWorking)
			end
	end
