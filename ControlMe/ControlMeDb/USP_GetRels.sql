CREATE PROCEDURE [dbo].[USP_GetRels]
	@MyID int
AS
SELECT
    STUFF(
        (
select ',['+UserName+']' AS [text()]
from
(
	SELECT [Screen Name] UserName 
	from Relationship r
	join Users u on r.SubID=u.Id
	where SubID=@MyID 
	union
	SELECT [Screen Name] UserName 
	from Relationship r
	join Users u on r.DomID=u.Id
	where SubID=@MyID 
	) as sub
	FOR XML PATH (''), TYPE
	).value('text()[1]','nvarchar(max)'), 1, 1, '') 
	
