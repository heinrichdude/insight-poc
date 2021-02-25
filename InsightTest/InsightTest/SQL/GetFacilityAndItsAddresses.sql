CREATE PROCEDURE GetFacilityAndItsAddresses
(
	@FacilityId int
)
AS
SELECT f.* 
FROM Facility AS f
WHERE f.FacilityId = @FacilityId

SELECT f.FacilityId, a.*
FROM Facility AS f 
JOIN Address AS a ON f.FacilityId = a.FacilityId
WHERE f.FacilityId = @FacilityId
