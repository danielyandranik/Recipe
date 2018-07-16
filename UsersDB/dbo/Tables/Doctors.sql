CREATE TABLE [dbo].[Doctors]
(
	[ProfileId] INT NOT NULL, 
    [License] NVARCHAR(50) NOT NULL, 
    [HospitalName] NVARCHAR(50) NOT NULL,
	[Specification] NVARCHAR(100) NOT NULL, 
    [WorkStartYear] NVARCHAR(50) NOT NULL, 
    [GraduatedYear] NVARCHAR(50) NOT NULL, 
    FOREIGN KEY (ProfileId) REFERENCES UserProfile(ProfileId)
)
