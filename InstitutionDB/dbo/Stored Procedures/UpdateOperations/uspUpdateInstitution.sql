CREATE PROCEDURE [dbo].[uspUpdateInstitution]
	@id int,
	@name nvarchar(100) = null, 
	@license nvarchar(100) = null,
	@owner nvarchar(100) = null,
    @phone nvarchar(100) = null, 
    @email nvarchar(100) = null, 
    @description nvarchar(MAX) = null,
    @openTime nvarchar(100) = null, 
    @closeTime nvarchar(100) = null,
	@type nvarchar(100) = null,
	@country nvarchar(100) = null,
	@state nvarchar(100) = null,
	@city nvarchar(100) = null,
	@postalCode nvarchar(100) = null,
	@addressLine nvarchar(100) = null
AS
	begin
		set @phone = ISNULL(@phone, (select phone from Institutions where Id = @id))
		set @email = ISNULL(@email, (select email from Institutions where Id = @id))
		set @description = ISNULL(@description, (select description from Institutions where Id = @id))
		
		update Institutions set
			Name = ISNULL(@name, name),
			License = ISNULL(@license, license),
			Owner = ISNULL(@owner, owner),
			Phone = IIF(@phone = '', null, @phone),
			Email = IIF(@email = '', null, @email),
			Description = IIF(@description = '', null, @description),
			OpenTime = ISNULL(@openTime, openTime),
			CloseTime = ISNULL(@closeTime, closeTime)
			where id =@id

			update Addresses set
			 Country = ISNULL(@country, country),
			 State = ISNULL(@state, state),
			 City = ISNULL(@city, city),
			 PostalCode = ISNULL(@postalCode, postalCode),
			 AddressLine = ISNULL(@addressLine, addressLine)
			 where  id = ( select addressId from Institutions where id = @id)
	end