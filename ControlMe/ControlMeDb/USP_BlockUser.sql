CREATE PROCEDURE [dbo].[USP_BlockUser]
	@blocker int = 0,
	@blockee int
AS
BEGIN
	insert into Block
	(BlockerId,
	BlockeeId)
	select @blocker,
	@blockee
END
