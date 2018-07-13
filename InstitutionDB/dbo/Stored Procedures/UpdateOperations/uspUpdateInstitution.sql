CREATE PROCEDURE [dbo].[uspUpdateInstitution]
	@id int,
	@name nvarchar(100) = null, 
	@license nvarchar(100) = null,
	@owner nvarchar(100) = null,
    @phone nvarchar(100) = null, 
    @email nvarchar(100) = null, 
    @description nvarchar(MAX) = null,
    @openTime time = null, 
    @closeTime time = null,
	@country nvarchar(100),
	@state nvarchar(100),
	@city nvarchar(100),
	@postalCode nvarchar(100),
	@addressLine nvarchar(100)
AS
	begin
		set @phone = ISNULL(@phone, (select phone from Institutions where Id = @id))
		set @email = ISNULL(@email, (select email from Institutions where Id = @id))
		set @description = ISNULL(@description, (select description from Institutions where Id = @id))
		
		update Institutions set
			@name = ISNULL(@name, name),
			@license = ISNULL(@license, license),
			@owner = ISNULL(@owner, owner),
			@phone = IIF(@phone = '', null, @phone),
			@email = IIF(@email = '', null, @email),
			@description = IIF(@description = '', null, @description),
			@openTime = ISNULL(@openTime, openTime),
			@closeTime = ISNULL(@closeTime, closeTime)
			where id =@id

			update Addresses set
			 @country = ISNULL(@country, country),
			 @state = ISNULL(@state, state),
			 @city = ISNULL(@city, city),
			 @postalCode = ISNULL(@postalCode, postalCode),
			 @addressLine = ISNULL(@addressLine, addressLine)
			 where  id = ( select addressId from Institutions where id = @id)


	end