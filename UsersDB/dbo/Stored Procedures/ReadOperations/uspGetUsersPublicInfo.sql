CREATE PROCEDURE [dbo].[uspGetUsersPublicInfo]
AS
	select Id,
		   FirstName,
		   LastName,
		   Username,
		   Sex
		   from Users
