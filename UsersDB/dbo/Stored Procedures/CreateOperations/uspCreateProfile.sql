CREATE PROCEDURE [dbo].[uspCreateProfile]
	@userId int,
	@type nvarchar(20),
	@createdDate Datetime,
	@isApproved bit = 0
AS
	begin
		insert into UserProfile values(@userId, @type, @createdDate, @isApproved)
	end

RETURN SCOPE_IDENTITY()