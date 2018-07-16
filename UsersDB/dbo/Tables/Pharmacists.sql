CREATE TABLE [dbo].[Pharmacists]
(
	[ProfileId] INT NOT NULL,
	[PharmacyId] INT NOT NULL,
	[PharmacyName] nvarchar(100),
	[StartedWorking] nvarchar(50),
	FOREIGN KEY (ProfileId) REFERENCES UserProfile(ProfileId)
)
