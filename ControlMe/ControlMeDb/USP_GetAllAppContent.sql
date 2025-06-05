CREATE PROCEDURE [dbo].[USP_GetAllAppContent]
@userID int = 0
AS
BEGIN

DELETE cmd FROM ControlAppCmd cmd
WHERE SenderId in (Select BlockeeId FROM Block where BlockerId=@userID)
AND SubId=@userID

DECLARE @Anon bit = 0
SELECT @Anon=AnonCmd from Users where ID=@userID

IF (@Anon=0)
BEGIN
DELETE cmd FROM ControlAppCmd cmd
WHERE SenderId =-1
AND SubId=@userID
END

SELECT cac.Id CacId, convert(varchar(100),SenderId) SenderId, cl.Content, case when r.Id is null then 'N' else 'D' end IsDom from ControlAppCmd cac
join CommandList cl
on cac.CmdId=cl.CmdId
left join Relationship r
on r.DomID=SenderId
and r.SubID=@userID
where cac.SubId=@userID
ORDER BY SendDate

END