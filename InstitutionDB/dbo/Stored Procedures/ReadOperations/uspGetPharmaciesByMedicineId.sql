CREATE PROCEDURE [dbo].[uspGetPharmaciesByMedicineId]
	@id int
AS
	begin
		select Institutions.Id, Name, License, Owner,  Phone, Email, Description, OpenTime, CloseTime, Type, 
					Country, State, City, PostalCode, AddressLine 
		from PharmacyMedicines join Institutions on PharmacyId = Institutions.Id 
								join Addresses on Institutions.AddressId = Addresses.Id
		where MedicineId = @id
	end