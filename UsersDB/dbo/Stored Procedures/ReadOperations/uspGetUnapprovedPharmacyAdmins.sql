CREATE PROCEDURE [dbo].[uspGetUnapprovedPharmacyAdmins]
AS
	select p.UserId, u.FullName, a.PharmacyName, p.CreatedDate, a.StartedWorking 
	from Users u
	join UserProfile p on u.Id = p.UserId
	join PharmacyAdmins a on p.ProfileId = a.ProfileId
	where p.[Type] = 'pharmacy_admin' and p.IsApproved = 0

RETURN 0
