CREATE PROCEDURE [uspCreatePharmacyAdminProfile]
	@userId int,
	@pharmacyId int,
	@pharmacyName nvarchar(100),
	@startedWorking nvarchar(50)
AS
	begin
		declare @profileId int
		execute @profileId = uspCreateProfile @userId, 'pharmacy_admin'

		if @profileId != 0
		   begin
				update UserProfile
					set IsApproved = 1
					where UserId = @userId and ProfileId = @profileId

				insert into PharmacyAdmins values(@profileId,@pharmacyId,@pharmacyName,@startedWorking)
		   end
	end
