CREATE PROCEDURE [dbo].[uspUpdateUser]
	@userId int,
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@email varchar(100),
	@password varchar(100),
	@phone varchar(50),
	@currentProfile nvarchar(20)
AS
	update Users 
		set FirstName = IIF(@firstName = null, FirstName, @firstname),
			LastName = IIF(@lastName = null, LastName, @lastName),
			Email = IIF(@email = null, Email, @email),
			[Password] = IIF(@password = null, [Password], @password),
			Phone = IIF(@phone = null, Phone, @phone),
			CurrentProfile = IIF(@currentProfile = null, CurrentProfile, @currentProfile)
	where Users.Id = @userId
