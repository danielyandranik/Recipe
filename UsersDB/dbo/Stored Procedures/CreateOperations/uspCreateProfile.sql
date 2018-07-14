CREATE PROCEDURE [dbo].[uspCreateProfile]
	@userId int,
	@type nvarchar(20)
AS
	begin
		insert into UserProfile values(@userId, @type, GetDate(), 0)
	end

RETURN SCOPE_IDENTITY()