CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Country] NVARCHAR(20) NOT NULL, 
    [State] NVARCHAR(20) NOT NULL, 
    [City] NVARCHAR(20) NOT NULL, 
    [PostalCode] NCHAR(10) NOT NULL, 
    [AddressLine] NVARCHAR(50) NOT NULL
)
