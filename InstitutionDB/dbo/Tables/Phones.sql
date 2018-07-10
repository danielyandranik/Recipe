CREATE TABLE [dbo].[Phones]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Phone] NCHAR(15) NOT NULL, 
    [Department] NVARCHAR(50) NOT NULL
)
