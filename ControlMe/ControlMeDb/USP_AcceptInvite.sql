CREATE PROCEDURE [dbo].[USP_AcceptInvite]
	@SubId int,
	@DomName varchar(250)
AS
	Insert into Relationship
	(DomId,SubID)
	select Id,@SubId
	from Users where [Screen Name]=@DomName

	Delete i from Invites i
	join Users u
	on i.DomId=u.Id
	where [Screen Name]=@DomName
	and i.SubId=@SubId

