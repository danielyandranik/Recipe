CREATE PROCEDURE [dbo].[uspGetPharmacyMedicineById]
	@id int
AS
	begin
		select Institutions.Name, MedicineId, Quantity, Price
		from PharmacyMedicines join Institutions on Institutions.Id = PharmacyId
		where PharmacyMedicines.Id = @id
	end
