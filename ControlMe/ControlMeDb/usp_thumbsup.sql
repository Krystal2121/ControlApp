CREATE PROCEDURE [dbo].[usp_thumbsup]
	@id int ,
	@senderid int
AS
	if(select count(*) from dbo.users where id=@senderid)>0
	BEGIN
		if @id!=@senderid
		BEGIN
		update users set ThumbsUp=ThumbsUp+1 where id=@senderid
		END

	END
RETURN 0
