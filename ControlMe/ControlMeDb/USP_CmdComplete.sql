CREATE PROCEDURE [dbo].[USP_CmdComplete]
	@userID int = 0
AS
BEGIN
	delete from ControlAppCmd where id =(
	SELECT top 1 id from [ControlAppCmd] c
join CommandList cl
on c.CmdId=cl.CmdId 
where SubId=@userID
	ORDER BY SendDate)
END