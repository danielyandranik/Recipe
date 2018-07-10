CREATE TABLE [dbo].[Institutions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(50) NOT NULL, 
	[Type] NVARCHAR(15) NOT NULL,
    [AddressId] INT NOT NULL, 
    [Email] VARCHAR(320) NULL, 
    [BusinessId] INT NOT NULL, 
    [Description] TEXT NULL,
	FOREIGN KEY (AddressId) REFERENCES Address(Id)
)
