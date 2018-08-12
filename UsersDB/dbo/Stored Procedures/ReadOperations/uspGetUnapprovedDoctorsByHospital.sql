CREATE PROCEDURE [dbo].[uspGetUnapprovedDoctorsByHospital]
	@hospital nvarchar(50)
AS
	begin
		select p.UserId,u.FullName, p.CreatedDate, d.License, d.Specification, d.WorkStartYear
			from Users u 
				join UserProfile p on u.Id = p.UserId
				join Doctors d on p.ProfileId = d.ProfileId
				where p.[Type] = 'doctor' and p.IsApproved = 0 and d.HospitalName = @hospital
	end	