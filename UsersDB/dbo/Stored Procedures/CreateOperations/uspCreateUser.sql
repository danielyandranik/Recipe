

CREATE PROCEDURE [dbo].[uspCreateUser]
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@middleName nvarchar(50) = null,
	@userName nvarchar(50),
	@birthdate date,
	@email varchar(100) = null,
	@password varchar(100),
	@phone varchar(50) = null,
	@sex nchar(1)
AS
	if not exists (select Username from dbo.Users where Username = @username)
	insert into dbo.Users 
		values (@firstName,@lastName,@middleName,@username,
				@birthdate,@email,@password,@phone,@sex,0,null)

	return scope_identity()