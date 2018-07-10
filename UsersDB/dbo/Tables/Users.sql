CREATE TABLE [dbo].[Users] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [MiddleName] NVARCHAR (50) NULL,
    [UserName]   NVARCHAR (50) NOT NULL,
    [Birthdate]  DATE          NOT NULL,
    [Email]      VARCHAR (100) NULL,
    [Password]   VARCHAR (100) NOT NULL,
    [Phone]      VARCHAR (50)  NULL,
    [Sex]        NCHAR (1)     NOT NULL,
    [IsActive]   BIT           NULL,
    [CurrentProfile] INT NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);



