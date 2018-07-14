CREATE PROCEDURE [dbo].[uspGetPharmaciesByMedicineId]
	@medicineId int
AS
	begin
		select Name
		from PharmacyMedicines join Institutions on PharmacyId = Institutions.Id
		where MedicineId = @medicineId
	end
