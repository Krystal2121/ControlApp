CREATE PROCEDURE [dbo].[USP_DeleteOutstanding]
	@userID int = 0
AS
BEGIN

		DELETE cmd FROM ControlAppCmd cmd
		WHERE SubId=@userID

END
