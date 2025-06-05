
CREATE PROCEDURE [dbo].[USP_SendAppCmd]
@usernm varchar(250),
@usercmd varchar(max),
@all int =0,
@username varchar(250) = null,
@password varchar(250) = null,
@fileloc varchar(max) = null
AS
BEGIN

declare @newidins int
Declare @senderid int =0
Declare @cmdid int =0
if (@username is not null)
BEGIN
	if (@username!='')
	BEGIN
		select @senderid=
		(
			select top 1 Id from
			(
				SELECT Id, [Role] from Users where [Login Name]=@UserName and Password=@Password
				union SELECT Id, [Role] from Users where [Screen Name]=@UserName and Password=@Password
			) as subq
		)
	END
	ELSE
	BEGIN
	SET @senderid=-1
	END
END
IF (@senderid!=0)
BEGIN
	declare @id int
	IF (@usernm='')
	BEGIN

			declare @numbers int
			SELECT @numbers=count(*) from Users where  [RandOpt]=1

			declare @which int
			select @which =cast(floor(rand()*@numbers) as int)+1

			SELECT @id=Id FROM Users
			ORDER BY Id
			OFFSET @which-1 ROWS
			FETCH NEXT 1 ROWS ONLY;

			insert into CommandList
			([Content])
			values
			(@usercmd)

			select @newidins=SCOPE_IDENTITY()

			insert into ControlAppCmd
			([SenderId],
			[SubId],
			[CmdId]
			)
			values (@senderid,@id,@newidins)

			insert into CmdLog
			([SenderId],[SubId],
			[Content])
			values (@senderid,@id,@usercmd)

	END
	ELSE
	IF ((ISNUMERIC(@usernm)=1) and (CONVERT(int,@usernm)<-1))
	BEGIN

			insert into CommandList
			([Content])
			values
			(@usercmd)

			select @newidins=SCOPE_IDENTITY()

		insert into ControlAppCmd
		([SenderId],
		[SubId],
		[CmdId],
		[GroupRefId])
		select @senderid,UserId, @newidins,CONVERT(int,@usernm)
		from UsersGroups
		where GroupRefId=CONVERT(int,@usernm)
		and UserId!=@senderid

		insert into CmdLog
		([SenderId],[SubId],
		[Content],
		[GroupRefId])
		values (@senderid,@senderid,@usercmd,CONVERT(int,@usernm))
	END
	ELSE
	BEGIN

		select @id=(
		select top 1 Id from
		( Select  Id from Users where [Screen Name]=@usernm union Select  Id from Users where convert(varchar(250),Id)=@usernm) sub1
		)
		if(@id is not null)
		BEGIN

			insert into CommandList
			([Content])
			values
			(@usercmd)

			select @newidins=SCOPE_IDENTITY()

			insert into ControlAppCmd
			([SenderId],[SubId],
			[CmdId])
			values (@senderid,@id,@newidins)

			insert into CmdLog
			([SenderId],[SubId],
			[Content])
			values (@senderid,@id,@usercmd)
		END
	END
END

delete c from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId
where [SenderId]<0
and [Content] like '%1mp7JCqszmk=|||%'

delete c from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId
where [SenderId]<0
and [Content] like ''

delete c from CommandList c where CmdId not in (select CmdId from [ControlAppCmd])

END