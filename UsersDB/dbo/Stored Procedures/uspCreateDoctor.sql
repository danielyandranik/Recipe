CREATE PROCEDURE [dbo].[uspCreateDoctor]
	@userId int,
	@license nvarchar(20),
	@hospitalId int
AS
	begin
		insert into Doctors values(@license, @hospitalId)

		declare @id int
		select @id = max(Id) from Doctors

		insert into UserProfile values(@userId, @id, 'doctor')
	end
