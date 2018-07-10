CREATE TABLE [dbo].[Pharmacies]
(
	[InstitutionId] INT NOT NULL,
    [PharmacyLicense] NVARCHAR(50) NOT NULL, 
    [Owner] NVARCHAR(50) NOT NULL, 
	FOREIGN KEY (InstitutionId) REFERENCES Institutions(Id)
)
