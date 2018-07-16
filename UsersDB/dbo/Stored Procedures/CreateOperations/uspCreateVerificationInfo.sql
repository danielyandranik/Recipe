CREATE PROCEDURE [dbo].[uspCreateVerificationInfo]
	@username nvarchar(50),
	@key varchar(50)
AS
	insert into Verifications values(@username,@key)
