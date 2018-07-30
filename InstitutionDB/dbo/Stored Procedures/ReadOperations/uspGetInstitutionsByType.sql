CREATE PROCEDURE [dbo].[uspGetInstitutionsByType]
	@type nvarchar(100)
AS
	begin
		select * from Institutions where type = @type
	end
