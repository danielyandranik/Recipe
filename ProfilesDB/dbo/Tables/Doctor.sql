CREATE TABLE [dbo].[DoctorProfile]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Type] NCHAR(10) NOT NULL, 
    [License] NVARCHAR(50) NOT NULL, 
    [HospitalId] INT NOT NULL
)
