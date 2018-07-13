CREATE PROCEDURE [dbo].[uspDeleteInstitution]
	@id int
AS
	begin
		delete from Addresses where id = (select AddressId from Institutions where Institutions.Id = @id)

		delete from Institutions where id = @id
	end
