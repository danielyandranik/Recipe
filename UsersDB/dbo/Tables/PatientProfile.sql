CREATE TABLE [dbo].[PatientProfile]
(
	[ProfileId] INT NOT NULL,
	FOREIGN KEY (ProfileId) REFERENCES UserProfile(ProfileId)
)
