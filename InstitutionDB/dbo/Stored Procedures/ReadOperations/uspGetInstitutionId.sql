CREATE PROCEDURE [dbo].[uspGetInstitutionId]
	@name nvarchar(100)
AS
	begin
		select Id from Institutions where Name = @name
	end
