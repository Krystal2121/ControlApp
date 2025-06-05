CREATE TABLE [dbo].[CmdLog]
(
	[Id] INT NOT NULL identity(1,1) PRIMARY KEY,
	[SenderID] int not null DEFAULT 0,
	[SubId] int not null,
	[Content] nvarchar(max) not null, 
    [GroupRefId] INT NULL
)
