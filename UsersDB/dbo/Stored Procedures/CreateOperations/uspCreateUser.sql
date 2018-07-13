CREATE PROCEDURE [dbo].[uspCreateUser]
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@middleName nvarchar(50) = null,
	@userName nvarchar(50),
	@fullName nvarchar(50) = null,
	@birthdate date,
	@email varchar(100) = null,
	@password varchar(100),
	@phone varchar(50) = null,
	@sex nchar(1)
AS
	begin
	if not exists (select Username from dbo.Users where Username = @username)
		begin
			insert into dbo.Users 
				values (@firstName,
						@lastName,
						@middleName,
						@fullName,
						@userName,
						@birthdate,
						@email,
						@password,@phone,
						@sex,
						0,0,'None',0,
						GetDate(),
						null)
		end
	end