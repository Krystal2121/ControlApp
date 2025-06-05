CREATE PROCEDURE [dbo].[USP_AddGroup]
	@user int,
	@group varchar(50)
AS
BEGIN

	declare @grouref int

	select @grouref=RefId
		from Groups
		where GroupName=@group

	if(select count(*) from [dbo].[UsersGroups] where [UserId]=@user and [GroupRefId]=@grouref)=0
	BEGIN
		insert into [dbo].[UsersGroups]
		(UserId,
		GroupRefId)
		select @user, @grouref

	END
	ELSE
	BEGIN

		delete from [dbo].[UsersGroups]
		where UserId=@User
		and GroupRefId=@grouref
	END
END