CREATE PROCEDURE [dbo].[uspCreatePharmacyMedicine]
	@id int,
	@pharmacyId int,
	@medicineId int,
	@quantity int,
	@price money
AS
	begin
		insert into PharmacyMedicines values(@pharmacyId, @medicineId, @quantity, @price)
	end
