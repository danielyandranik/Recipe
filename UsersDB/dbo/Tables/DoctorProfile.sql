CREATE TABLE [dbo].[DoctorProfile]
(
	[ProfileId] INT NOT NULL, 
    [License] NVARCHAR(50) NOT NULL, 
    [HospitalId] INT NOT NULL,
	FOREIGN KEY (ProfileId) REFERENCES UserProfile(ProfileId)
)
