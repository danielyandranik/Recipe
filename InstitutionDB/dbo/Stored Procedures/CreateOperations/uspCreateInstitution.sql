﻿CREATE PROCEDURE [dbo].[uspCreateInstitution]
	@name nvarchar(100), 
	@license nvarchar(100),
	@owner nvarchar(100),
    @phone nvarchar(100) = null, 
    @email nvarchar(100) = null, 
    @description nvarchar(MAX) = null,
    @openTime time, 
    @closeTime time ,
	@type nvarchar(100),
	@country nvarchar(100),
	@state nvarchar(100),
	@city nvarchar(100),
	@postalCode nvarchar(100),
	@addressLine nvarchar(100)
AS
	begin
		declare @addressId int
		set @addressId = null

		select @addressId = Id from [Addresses] where   
					Country = @country 
				and [State] = @state
				and City = @city
				and PostalCode = @postalCode
				and AddressLine = @addressLine

			if @addressId = null
				begin
					insert into [Addresses] values (@country, @state, @city, @postalCode, @addressLine)
					set @addressId = Scope_Identity()
				end


		insert into Institutions 
			values(@name, @license, @owner, @addressId,
					@phone, @email, @description, @openTime, @closeTime, @type)

		return scope_identity()
	end

	