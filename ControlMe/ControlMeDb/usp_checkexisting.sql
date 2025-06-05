CREATE PROCEDURE [dbo].[usp_checkexisting]
	@username varchar(max), @email varchar(max)
AS
	select * from Users where [Screen Name]=@username or [Login Name]=@email

