CREATE TABLE [dbo].[PharmacistProfile]
(
	[ProfileId] INT NOT NULL,
    [PharmacyId] INT NOT NULL,
	FOREIGN KEY (ProfileId) REFERENCES UserProfile(ProfileId)
)
