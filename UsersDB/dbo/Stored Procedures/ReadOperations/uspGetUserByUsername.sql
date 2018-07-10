

CREATE PROCEDURE [dbo].[uspGetUserByUsername]
	@username nvarchar(50)
AS
	SELECT * from dbo.Users where UserName = @username
RETURN 0