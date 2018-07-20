CREATE PROCEDURE [dbo].[uspGetInstitutionById]
	@id int
AS
	begin
		select Institutions.Id, Name, License, Owner,  Phone, Email, Description, OpenTime, CloseTime, Type, 
					Country, State, City, PostalCode, AddressLine 
		from Institutions join Addresses on Institutions.AddressId = Addresses.Id
		where Institutions.Id = @id
	end
