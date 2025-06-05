CREATE PROCEDURE [dbo].[USP_Register]
	@ScreenName varchar(255),
	@Email varchar(255),
	@Password varchar(255),
	@Role varchar(3),
	@opt int
AS
	insert into Users ([Screen Name], [Login Name], [Password],[Role], [RandOpt])
	select @ScreenName, @Email, @Password, @Role, @opt

