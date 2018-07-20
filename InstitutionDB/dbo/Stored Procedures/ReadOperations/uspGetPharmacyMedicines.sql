CREATE PROCEDURE [dbo].[uspGetPharmacyMedicines]
	@id int
AS
	begin
		select *
		from PharmacyMedicines join Institutions on Institutions.Id = PharmacyId
		where PharmacyId = @id
	end
GO