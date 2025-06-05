CREATE TABLE [dbo].[ChatLog]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	ReceiverID int,
	SenderID int,
	ChatTxt varchar(max),
	Date_Add datetime default getdate()
)
