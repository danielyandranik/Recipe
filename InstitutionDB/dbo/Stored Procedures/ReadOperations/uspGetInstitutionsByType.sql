CREATE PROCEDURE [dbo].[uspGetInstitutionsByType]
	@type nvarchar(100)
AS
	begin
		select Institutions.Id, Name, License, Owner,  Phone, Email, Description, OpenTime, CloseTime, Type, 
					Country, State, City, PostalCode, AddressLine 
		from Institutions join Addresses on Institutions.AddressId = Addresses.Id
		where type = @type
	end
