CREATE PROCEDURE [dbo].[uspCreateInstitution]
	@id int,
	@name nvarchar(100), 
	@license nvarchar(100),
	@owner nvarchar(100),
    @phone nvarchar(100) = null, 
    @email nvarchar(100) = null, 
    @description nvarchar(MAX) = null,
    @openTime nvarchar(100), 
    @closeTime nvarchar(100) ,
	@type nvarchar(100),
	@address nvarchar(MAX)
AS
	begin
		if not exists(select * from Institutions where Address = @address)
			begin 
				insert into Institutions 
				values(@name, @license, @owner, @address,
						@phone, @email, @description, @openTime, @closeTime, SUBSTRING(@type, 39, 8))
			end
	end

	