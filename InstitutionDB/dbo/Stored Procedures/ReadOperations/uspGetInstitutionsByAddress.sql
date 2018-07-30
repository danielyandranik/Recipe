CREATE PROCEDURE [dbo].[uspGetInstitutionsByAddress]
	@address nvarchar(MAX),
	@type nvarchar(100)
as
	begin
		SELECT * from Institutions where Address LIKE '%' + @address + '%' and Type = @type
	end