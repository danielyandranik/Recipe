

CREATE PROCEDURE [dbo].[uspUpdateUser]
	@userId int,
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@email varchar(100),
	@password varchar(100),
	@phone varchar(50),
	@currentProfile nvarchar(20)
AS
	if @firstName != null
		update dbo.Users set FirstName = @firstName
	if @lastName != null
		update dbo.Users set LastName = @lastName
	if @email != null
		update dbo.Users set Email = @email
	if @password != null
		update dbo.Users set Password = @password
	if @phone != null
		update dbo.Users set Phone = @phone
	if @currentProfile != null
		update dbo.Users set CurrentProfile = @currentProfile

RETURN 0