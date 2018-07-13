CREATE PROCEDURE [dbo].[uspUpdateUserPublicInfo]
	@userId int,
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@middleName nvarchar(50),
	@fullName nvarchar(50),
	@email varchar(100),
	@phone varchar(50)
AS
	update Users 
		set FirstName = IIF(@firstName = null, FirstName, @firstname),
			LastName = IIF(@lastName = null, LastName, @lastName),
			MiddleName = IIF(@middleName = null,MiddleName,@middleName),
			FullName = IIF(@fullName = null,FullName,@fullName),
			Email = IIF(@email = null, Email, @email),
			Phone = IIF(@phone = null, Phone, @phone)
	where Users.Id = @userId
