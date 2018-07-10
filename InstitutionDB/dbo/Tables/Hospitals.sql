CREATE TABLE [dbo].[Hospitals]
(
	[InstitutionId] INT NOT NULL, 
    
	[HospitalLicense] NVARCHAR(50) NOT NULL, 
    FOREIGN KEY (InstitutionId) REFERENCES Institutions(Id)
)
