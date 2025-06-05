CREATE PROCEDURE [dbo].[usp_userinfo]
	@username varchar(255)='',
	@id int=0
AS
BEGIN
	if @username!=''
	begin
		select * from users where [Screen Name]=@username
	END
	ELSE if @id!=0
	BEGIN
		select * from users where id=@id
	END
END
