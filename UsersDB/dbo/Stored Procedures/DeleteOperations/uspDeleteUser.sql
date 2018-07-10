CREATE PROCEDURE [dbo].[uspDeleteUser]
	@userId int
AS
begin
	execute uspDeleteDoctorProfile @userId

	execute uspDeletePatientProfile @userId

	execute uspDeletePharmacistProfile @userId

	delete from dbo.Users where Users.Id = @userId
end