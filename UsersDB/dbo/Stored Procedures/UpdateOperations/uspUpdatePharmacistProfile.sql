CREATE PROCEDURE [dbo].[uspUpdatePharmacistProfile]
	@userId int,
	@pharmacyId int = null,
	@pharmacyName nvarchar(100),
	@startedWorking nvarchar(50)
AS
begin
	declare @profileId int

	select @profileId = UserProfile.ProfileId from UserProfile 
		where UserProfile.UserId = @userId and [Type] = 'pharmacist'

	update Pharmacists set 
		PharmacyId = IIF(@pharmacyId = null, PharmacyId, @pharmacyId),
		PharmacyName = IIF(@pharmacyName = null,PharmacyName,@pharmacyName),
		StartedWorking = IIF(@startedWorking = null,StartedWorking,@startedWorking)
		where Pharmacists.ProfileId = @profileId
end
