CREATE PROCEDURE [dbo].[uspGetPharmacyMedicines]
	@pharmacyId int
AS
	begin
		select MedicineId, Quantity, Price
		from PharmacyMedicines join Institutions on Institutions.Id = PharmacyId
		where PharmacyId = @pharmacyId
	end
