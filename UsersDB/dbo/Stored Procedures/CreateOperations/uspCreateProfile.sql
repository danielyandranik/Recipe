CREATE PROCEDURE [dbo].[uspCreateProfile]
	@userId int,
	@type nvarchar(20)
AS
	begin
		if not exists (select [Type] from UserProfile where UserId = @userId and [Type] = @type)
			begin
				insert into UserProfile values(@userId, @type, GetDate(), 0)
				return SCOPE_IDENTITY()
			end
		else
			return 0
	end

