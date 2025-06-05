CREATE PROCEDURE [dbo].[usp_bestsender]

AS
	SELECT top 5
	[Screen Name],
	[ThumbsUp]
	from users
	order by ThumbsUp desc
RETURN 0
