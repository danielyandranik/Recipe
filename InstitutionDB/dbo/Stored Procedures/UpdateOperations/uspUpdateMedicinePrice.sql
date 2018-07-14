CREATE PROCEDURE [dbo].[uspUpdateMedicinePrice]
	@id int,
	@price money
AS
	update PharmacyMedicines
	set price = @price where Id = @id
RETURN 0
