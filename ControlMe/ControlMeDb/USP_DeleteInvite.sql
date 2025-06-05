CREATE PROCEDURE [dbo].[USP_DeleteInvite]
	@SubId int,
	@DomName varchar(250)
AS
	Delete i from Invites i
	join Users u
	on i.DomId=u.Id
	where [Screen Name]=@DomName
	and i.SubId=@SubId