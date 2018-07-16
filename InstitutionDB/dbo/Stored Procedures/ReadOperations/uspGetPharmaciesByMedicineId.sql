CREATE PROCEDURE [dbo].[uspGetPharmaciesByMedicineId]
	@medicineId int
AS
	begin
		select Name, License, Owner,  Phone, Email, Description, OpenTime, CloseTime, 
					Country, State, City, PostalCode, AddressLine 
		from PharmacyMedicines join Institutions on PharmacyId = Institutions.Id 
								join Addresses on Institutions.AddressId = Addresses.Id
		where MedicineId = @medicineId
	end