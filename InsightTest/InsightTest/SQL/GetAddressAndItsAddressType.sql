CREATE PROCEDURE [dbo].GetAddressAndAddressType
(
	@AddressId int
)
AS

SELECT a.*, at.* 
FROM dbo.Address as a 
JOIN dbo.AddressType as at ON a.AddressTypeId = at.AddressTypeId
WHERE a.AddressId=@AddressId

GO
