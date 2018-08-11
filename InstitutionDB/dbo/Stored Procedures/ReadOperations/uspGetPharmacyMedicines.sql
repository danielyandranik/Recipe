CREATE PROCEDURE [dbo].[uspGetPharmacyMedicines]
	@id int
AS
	begin
		select * from PharmacyMedicines where PharmacyId = @id
	end
GO