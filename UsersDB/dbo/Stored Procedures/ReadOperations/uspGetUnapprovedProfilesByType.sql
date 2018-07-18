CREATE PROCEDURE [dbo].[uspGetUnapprovedProfilesByType]
	@type nvarchar(20)
AS
	select * from UserProfile
		where [Type] = @type and IsApproved = 0
