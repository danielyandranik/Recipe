CREATE PROCEDURE [dbo].[uspApproveProfile]
	@userId int,
	@type nvarchar(50)
AS
	update UserProfile set
		IsApproved = 1
		where UserId = @userId and [Type] = @type
