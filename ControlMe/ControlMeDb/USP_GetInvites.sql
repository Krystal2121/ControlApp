CREATE PROCEDURE [dbo].[USP_GetInvites]
	@SubID int 
AS
	SELECT u.[Screen Name] DomUser 
	from Invites i
	join Users u
	on i.DomId=u.Id
	where SubId=@SubId

