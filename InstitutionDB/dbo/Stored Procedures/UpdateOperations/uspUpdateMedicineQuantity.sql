CREATE PROCEDURE [dbo].[uspUpdateMedicineQuantity]
	@id int,
	@quantity int
AS
	update PharmacyMedicines
	set Quantity = @quantity where id = @id
RETURN 0
