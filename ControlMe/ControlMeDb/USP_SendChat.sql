CREATE PROCEDURE [dbo].[USP_SendChat]
	@SenderId int ,
	@ReceiverId int,
	@message varchar(max),
	@RecUserName varchar(250)=''
AS

	declare @newother int
	if @RecUserName!=''
	BEGIN
		select @newother=ID 
		FROM Users
		where [Screen Name]=  @RecUserName
	END
	ELSE
	BEGIN
		SET @newother=@ReceiverId
	END

	Insert into ChatLog
	(ReceiverID ,
	SenderID ,
	ChatTxt)
	values
	(@newother,
	@SenderId,
	@message)

