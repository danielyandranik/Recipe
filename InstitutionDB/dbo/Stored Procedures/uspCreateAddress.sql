CREATE PROCEDURE [dbo].[uspCreateAddress]
	@country nvarchar(20),
	@state nvarchar(20),
	@city nvarchar(20),
	@postalCode nchar(10),
	@addressLine nvarchar(100)
AS
	begin
		declare @addressId int
		set @addressId = null

		select @addressId = Id from [Address] where   
					Country = @country 
				and [State] = @state
				and City = @city
				and PostalCode = @postalCode
				and AddressLine = @addressLine

			if @addressId = null
				begin
					insert into [Address] values (@country, @state, @city, @postalCode, @addressLine)
					return Scope_Identity()
				end
			else
				return @addressId
	end

