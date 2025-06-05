CREATE PROCEDURE [dbo].[USP_GetUserSettings]
	@userid varchar(max)
AS
	SELECT * from Users where Id=@userid
RETURN 0
