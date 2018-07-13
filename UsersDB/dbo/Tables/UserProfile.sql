CREATE TABLE [dbo].[UserProfile]
(
    [ProfileId] INT NOT NULL IDENTITY(1, 1), 
    [UserId] INT NOT NULL, 
    [Type] NVARCHAR(20) NOT NULL, 
    [CreatedDate] DATE NOT NULL, 
    [IsApproved] BIT NOT NULL, 
    CONSTRAINT [PK_UserProfile] PRIMARY KEY ([ProfileId]),
	FOREIGN KEY (UserId) REFERENCES Users(Id)
)
