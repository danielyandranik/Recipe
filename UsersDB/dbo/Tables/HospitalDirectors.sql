CREATE TABLE [dbo].[HospitalDirectors]
(
	[ProfileId] INT NOT NULL references UserProfile(ProfileId),
	[HospitalName] nvarchar(100),
	[Occupation] nvarchar(100),
	[StartedWorking] nvarchar(100)
)
