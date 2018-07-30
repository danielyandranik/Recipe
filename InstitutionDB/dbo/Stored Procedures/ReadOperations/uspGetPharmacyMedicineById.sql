CREATE PROCEDURE [dbo].[uspGetPharmacyMedicineById]
	@id int
AS
	begin
		select* from PharmacyMedicines where Id = @id
	end