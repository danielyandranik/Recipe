CREATE PROCEDURE [dbo].[uspDeletePharmacyMedicine]
	@id int
AS
	begin
		delete from PharmacyMedicines where id = @id
	end
