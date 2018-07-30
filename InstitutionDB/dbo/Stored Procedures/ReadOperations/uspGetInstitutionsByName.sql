CREATE PROCEDURE [dbo].[uspGetInstitutionsByName]
	@name nvarchar(100),
	@type nvarchar(100)
as
	begin
		SELECT * from Institutions where Name LIKE '%' + @name + '%' and Type = @type
	end
