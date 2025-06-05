CREATE PROCEDURE [dbo].[USP_GetSub]
	@DomID int
AS
	SELECT [Screen Name] UserName, SubID from Relationship r
	join Users u on r.SubID=u.Id
	where DomID=@DomID 

