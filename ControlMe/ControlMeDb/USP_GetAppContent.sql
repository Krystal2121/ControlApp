CREATE PROCEDURE [dbo].[USP_GetAppContent]
@userID int = 0
AS
BEGIN

DELETE cmd FROM ControlAppCmd cmd
WHERE SenderId in (Select BlockeeId FROM Block where BlockerId=@userID)
AND SubId=@userID

DELETE cmd FROM ControlAppCmd cmd
WHERE SenderId =-1
AND SubId=@userID


SELECT TOP 1 convert(varchar(100),SenderId),u.[Screen Name] SenderName, cl.Content
from ControlAppCmd cac
join CommandList cl
on cac.CmdId=cl.CmdId
join Users u
on SenderId=u.Id
where cac.SubId=@userID
ORDER BY SendDate

END