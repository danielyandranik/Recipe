CREATE TABLE [dbo].[InstitutionPhone]
(
	[InstitutionId] INT NOT NULL, 
    [PhoneId] INT NOT NULL,
	FOREIGN KEY (InstitutionId) REFERENCES Institutions(Id),
	FOREIGN KEY (PhoneId) REFERENCES Phones(Id)
)
