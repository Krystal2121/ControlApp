CREATE PROCEDURE [dbo].[USP_AddInvite]
	@DomId int,
	@SubName varchar(250)
AS
	Insert into Invites
	(SubId,
	DomId)
	Select u.Id,
	@DomId
	From Users u
	where [Screen Name]=@SubName
RETURN 0
