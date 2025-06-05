CREATE PROCEDURE [dbo].[usp_requestvar]
	@ID int 
AS
BEGIN
	
	declare @counting int

	select @counting=count(*) from VarificationReq where SubID=@ID
	if @counting=0
	BEGIN
		insert into VarificationReq
		([SubID])
		values
		(@ID)
	END
	select [Login Name],VarifiedCode from Users where ID = @ID

END
