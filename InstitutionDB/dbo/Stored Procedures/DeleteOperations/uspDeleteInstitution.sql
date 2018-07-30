CREATE PROCEDURE [dbo].[uspDeleteInstitution]
	@id int
AS
	begin
		delete from Institutions where id = @id
	end
