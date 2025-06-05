CREATE PROCEDURE [dbo].[USP_GetInvites2]
	@SubID int 
AS

SELECT
    STUFF(
        (
select ',['+DomUser+']' AS [text()]
from
(
	SELECT u.[Screen Name] DomUser 
	from Invites i
	join Users u
	on i.DomId=u.Id
	where SubId=@SubId
		) as sub
	FOR XML PATH (''), TYPE
	).value('text()[1]','nvarchar(max)'), 1, 1, '') 
