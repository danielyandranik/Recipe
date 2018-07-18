CREATE PROCEDURE [dbo].[uspCreateMinistryWorkerProfile]
	@userId int,
	@position nvarchar(50),
	@startedWorking nvarchar(50)
AS
	begin
		declare @profileId int
		execute @profileId = dbo.uspCreateProfile @userId,'ministryWorker'

		insert into MinistryWorkers 
			values(@profileId,@position,@startedWorking)
	end
