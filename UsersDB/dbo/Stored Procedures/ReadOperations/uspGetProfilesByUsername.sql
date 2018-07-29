CREATE PROCEDURE [dbo].[uspGetProfilesByUsername]
	@username nvarchar (50)
AS
   begin
		declare @id int
		
		select @id = Id from Users where Username = @username

		exec dbo.uspGetProfilesByUserId @id
   end
