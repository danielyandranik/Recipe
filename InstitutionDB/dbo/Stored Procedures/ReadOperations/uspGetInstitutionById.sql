CREATE PROCEDURE [dbo].[uspGetInstitutionById]
	@id int
AS
	begin
		select * from Institutions where Institutions.Id = @id
	end
