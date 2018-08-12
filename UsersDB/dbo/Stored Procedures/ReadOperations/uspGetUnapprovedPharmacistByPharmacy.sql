CREATE PROCEDURE [dbo].[uspGetUnapprovedPharmacistByPharmacy]
	@pharmacy nvarchar(50)
AS
begin
	select p.UserId, u.FullName, p.CreatedDate as 'ProfileCreatedOn', ph.PharmacyName, ph.StartedWorking
	from Users u 
	join UserProfile p on u.Id = p.UserId
	join Pharmacists ph on p.ProfileId = ph.ProfileId
	where p.[Type] = 'pharmacist' and p.IsApproved = 0 and ph.PharmacyName = @pharmacy
end	