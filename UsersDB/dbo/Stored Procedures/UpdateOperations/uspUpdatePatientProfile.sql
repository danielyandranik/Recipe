CREATE PROCEDURE [dbo].[uspUpdateatientProfile]
	@userId int
AS
begin
	declare @profileId int

	select @profileId = UserProfile.ProfileId from UserProfile where UserProfile.UserId = @userId

	
end
