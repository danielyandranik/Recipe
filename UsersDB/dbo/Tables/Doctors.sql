CREATE TABLE [dbo].[Doctors]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1, 1), 
    [License] NVARCHAR(50) NOT NULL, 
    [HospitalId] INT NOT NULL
)
