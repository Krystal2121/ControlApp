CREATE PROCEDURE [dbo].[USP_GetDom]
	@SubID int
AS
	SELECT [Screen Name] UserName, DomID from Relationship r
	join Users u on r.DomID=u.Id
	where SubID=@SubID 

