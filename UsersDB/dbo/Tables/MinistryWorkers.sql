CREATE TABLE [dbo].[MinistryWorkers]
(
	[ProfileId] INT NOT NULL foreign key references UserProfile(ProfileId),
	[Position] nvarchar(50),
	[StartedWorking] nvarchar(50)
)
