CREATE TABLE [dbo].[PharmacyMedicines]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PharmacyId] INT NOT NULL, 
    [MedicineId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] MONEY NOT NULL
)
