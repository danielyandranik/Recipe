CREATE PROCEDURE [dbo].[uspCreateInstitution]
	@name nvarchar(50),
	@type nvarchar(15),
	@email varchar(320),
	@description text,
	@country nvarchar(20),
	@state nvarchar(20),
	@city nvarchar(20),
	@postalCode nchar(10),
	@addressLine nvarchar(100)
AS
	