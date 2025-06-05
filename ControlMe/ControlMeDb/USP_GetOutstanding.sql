CREATE PROCEDURE [dbo].[USP_GetOutstanding]
	@userID int = 0
AS
BEGIN

	DELETE cmd FROM ControlAppCmd cmd
	WHERE SenderId in (Select BlockeeId FROM Block where BlockerId=@userID)
	AND SubId=@userID

	DECLARE @Anon bit = 0
	DECLARE @vari bit=0
	DECLARE @Thumbs int=0
	SELECT @Anon=AnonCmd, @vari=Varified , @Thumbs=ThumbsUp from Users where ID=@userID

	IF (@Anon=0)
	BEGIN
		DELETE cmd FROM ControlAppCmd cmd
		WHERE SenderId =-1
		AND SubId=@userID
	END

	declare @whonext varchar(20)

	select top 1 @whonext= isnull(GroupRefId,SenderId)
	from [ControlAppCmd] c
	join CommandList cl
	on c.CmdId=cl.CmdId
	where SubId=@userID order by [SendDate]

	update Users
	set LoginDate=getdate()
	where ID=@userID

	if Convert(int,@whonext )<-1
	begin
		select @whonext= 'Group: '+ GroupName from Groups where RefId=@whonext
	end
	else
	begin
		if Convert(int,@whonext )=-1
		begin
			set @whonext='Anon'
		end
		else
		begin
			set @whonext=''
			select @whonext=u.[Screen Name] from users u join Relationship r on u.Id=r.DomID
			where r.SubID=@userID
			if(@whonext='')
				set @whonext='User'
		end
	end
	SELECT convert(varchar(100),count(*)) counting,  @whonext whonext, @vari varified, @Thumbs Thumbs  from ControlAppCmd where SubId=@userID
END