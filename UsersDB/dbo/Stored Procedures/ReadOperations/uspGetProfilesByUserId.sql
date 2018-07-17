CREATE PROCEDURE [dbo].[uspGetProfilesByUserId]
	@userId int
AS
	select * from UserProfile where UserId = @userId
