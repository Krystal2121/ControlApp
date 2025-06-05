CREATE PROCEDURE [dbo].[USP_Login]
	@UserName varchar(255),
	@Password varchar(255)
AS
BEGIN
	SELECT Id, [Role],Varified from Users where [Screen Name]=@UserName and Password=@Password
END
