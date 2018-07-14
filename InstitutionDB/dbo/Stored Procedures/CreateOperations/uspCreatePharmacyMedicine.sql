CREATE PROCEDURE [dbo].[uspCreatePharmacyMedicine]
	@institutionId int,
	@medicineId int,
	@quantity int,
	@price money
AS
	begin
		insert into PharmacyMedicines values(@institutionId, @medicineId, @quantity, @price)

		return scope_identity()
	end
