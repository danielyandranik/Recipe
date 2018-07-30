CREATE PROCEDURE [dbo].[uspGetPharmaciesByMedicineId]
	@id int
AS
	begin
		select Institutions.Id, Name, License, Owner, Address, Phone, Email, Description, OpenTime, CloseTime, Type
		from PharmacyMedicines join Institutions on PharmacyId = Institutions.Id 
		where MedicineId = @id
	end