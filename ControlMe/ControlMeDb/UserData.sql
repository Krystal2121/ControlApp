CREATE TABLE [dbo].[UserData]
(
	[Id] INT NOT NULL PRIMARY KEY,
	CommandId INT,
	NoViews INT,
	NoThumbsUp INT,
	NOThumbsDown INT,
	Content varchar(max)
)
