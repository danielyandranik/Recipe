CREATE TABLE [dbo].[UserProfile]
(
	[UserId] INT NOT NULL, 
    [ProfileId] INT NOT NULL,
	FOREIGN KEY (ProfileId) REFERENCES Profiles(Id)
)
