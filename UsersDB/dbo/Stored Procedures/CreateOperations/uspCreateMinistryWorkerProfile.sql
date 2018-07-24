CREATE PROCEDURE [dbo].[uspCreateMinistryWorkerProfile]
	@userId int,
	@position nvarchar(50),
	@startedWorking nvarchar(50)
AS
	begin
		declare @profileId int
		execute @profileId = dbo.uspCreateProfile @userId,'ministry_worker'

		if @profileId != 0
			begin
				insert into MinistryWorkers 
					values(@profileId,@position,@startedWorking)
			end
	end
