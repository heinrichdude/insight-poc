CREATE PROCEDURE [dbo].GetAddressAndAddressType
(
	@AddressId int
)
AS

SELECT a.* FROM dbo.Address as a 
WHERE a.AddressId=@AddressId

SELECT at.* FROM dbo.AddressType as at
JOIN dbo.Address as a on at.AddressTypeId = a.AddressTypeId
WHERE a.AddressId=@AddressId

GO
