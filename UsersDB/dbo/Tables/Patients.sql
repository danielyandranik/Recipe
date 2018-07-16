CREATE TABLE [dbo].[Patients]
(
	[ProfileId] INT NOT NULL,
	[RegionalDoctorName] NVARCHAR(50) NULL, 
    [Occupation] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(50) NULL, 
    [IsAlcohоlic] BIT NULL, 
    [IsDrugAddicted] BIT NULL, 
    FOREIGN KEY (ProfileId) REFERENCES UserProfile(ProfileId)
)
