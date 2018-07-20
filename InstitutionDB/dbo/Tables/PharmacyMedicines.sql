CREATE TABLE [dbo].[PharmacyMedicines]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [PharmacyId] INT NOT NULL, 
    [MedicineId] INT NOT NULL, 
    [Quantity] INT NOT NULL CHECK( [Quantity] >= 0), 
    [Price] MONEY NOT NULL CHECK( [Price] >= 0),
    FOREIGN KEY (PharmacyId) REFERENCES Institutions(Id)

)
