CREATE PROCEDURE [dbo].[USP_GetChatLog]
	@RequestID int ,
	@OtherID int,
	@OtherUser varchar(255) = ''
AS
BEGIN

	declare @newother int
	if @OtherUser!=''
	BEGIN
		select @newother=ID 
		FROM Users
		where [Screen Name]=  @OtherUser
	END
	ELSE
	BEGIN
		SET @newother=@OtherID
	END

	select * from (
	SELECT 'S' Type,ChatTxt,Date_Add
	FROM ChatLog
	where SenderID=@RequestID
	and ReceiverID=@newother

	union

	SELECT 'R' Type,ChatTxt,Date_Add
	FROM ChatLog
	where ReceiverID=@RequestID
	and SenderID=@newother
	) as sub 
	Order by Date_Add
END
