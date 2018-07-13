CREATE PROCEDURE [dbo].[uspGetUserPersonalInfoById]
	@id int
AS
	select Id,FirstName,LastName,MiddleName,FullName,UserName,Birthdate,
			Email,Phone,Sex,CurrentProfileType,
			RegisterDate,VerifiedDate
			from Users where Id = @id
