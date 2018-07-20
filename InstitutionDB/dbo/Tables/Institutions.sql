CREATE TABLE [dbo].[Institutions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(100) NOT NULL, 
	[License] NVARCHAR(100) NOT NULL, 
	[Owner] NVARCHAR(100) NOT NULL,
    [AddressId] INT NOT NULL, 
    [Phone] NVARCHAR(100) NULL, 
    [Email] NVARCHAR(100) NULL, 
    [Description] NVARCHAR(MAX) NULL,
    [OpenTime] NVARCHAR(100) NOT NULL, 
    [CloseTime] NVARCHAR(100) NOT NULL, 
    [Type] NVARCHAR(100) NOT NULL, 
    FOREIGN KEY (AddressId) REFERENCES Addresses(Id)
)
