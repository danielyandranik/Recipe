CREATE PROCEDURE [dbo].[uspGetInstitutionsByAddress]
	@address nvarchar(MAX)
as
	begin
		SELECT * from Institutions where Address LIKE '%' + @address + '%'
	end