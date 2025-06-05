CREATE PROCEDURE [dbo].[usp_varify]
	@ID int,
	@varcode int
AS
BEGIN

	declare @tries int
	select @tries=Count from VarificationReq where SubID=@ID

	if @tries<=10
	BEGIN
		update VarificationReq
		set VarificationCode=@varcode
		where SubID=@ID

		declare @counting int
		select @counting=count(*) from Users u join VarificationReq vr on u.Id=vr.SubID and u.VarifiedCode=vr.VarificationCode

		if @counting=1
		BEGIN
			delete from VarificationReq where SubID=@ID
			update Users set Varified=1 where Id=@ID
			select 'Done'
		END
		else
		BEGIN
			UPDATE VarificationReq
			SET Count=Count+1
			where SubID=@ID
			select 'Incorrect'
		END
	END
	ELSE
	BEGIN
		select 'Too many tried'
	END
END
