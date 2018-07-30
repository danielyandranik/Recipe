CREATE PROCEDURE [dbo].[uspCreatePharmacyMedicine]
	@id int,
	@pharmacyId int,
	@medicineId int,
	@quantity int,
	@price money
AS
	begin
		if not exists(select * from PharmacyMedicines where PharmacyId = @pharmacyId and MedicineId = @medicineId)
			begin
				insert into PharmacyMedicines 
				values(@pharmacyId, @medicineId, @quantity, @price)
			end
	end
