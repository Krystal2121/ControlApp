CREATE PROCEDURE [dbo].[USP_UpdateSettings]
	@ID int,
	@optin bit, 
	@password varchar(max),
	@Anon bit,
	@email varchar(max)=null
AS

	declare @oldemail varchar(max)
	declare @curvar bit
	select @oldemail=[Login Name],@curvar=Varified from Users
	where ID=@ID

	if @oldemail!=@email
	BEGIN
		set @curvar=0
		update Users
		set VarifiedCode=rand()*1000
		where ID=@ID
	END

	update Users
	set RandOpt=@optin,Varified=@curvar, Password=@password, AnonCmd=@Anon, [Login Name]=case when @email is null then [Login Name] else @email end
	where ID=@ID

