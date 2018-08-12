CREATE PROCEDURE [dbo].[uspGetUnapprovedPharmacyAdmins]
AS
<<<<<<< HEAD
	select p.UserId, u.FullName, a.PharmacyName, p.CreatedDate as 'ProfileCreatedOn', a.StartedWorking 
	from Users u
	join UserProfile p on u.Id = p.UserId
	join PharmacyAdmins a on p.ProfileId = a.ProfileId
	where p.[Type] = 'pharmacy_admin' and p.IsApproved = 0

=======
	begin
		select p.UserId, u.FullName, a.PharmacyName, p.CreatedDate, a.StartedWorking 
			from Users u
				join UserProfile p on u.Id = p.UserId
				join PharmacyAdmins a on p.ProfileId = a.ProfileId
				where p.[Type] = 'pharmacy_admin' and p.IsApproved = 0
	end
>>>>>>> 8fd1918033ece5ea8dc2b893fe078dc12ef4d5bf
RETURN 0
